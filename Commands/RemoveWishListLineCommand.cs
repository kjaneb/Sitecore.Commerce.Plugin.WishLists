using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Core.Commands;
using Sitecore.Framework.Pipelines;

namespace Sitecore.Commerce.Plugin.WishLists
{
    public class RemoveWishListLineCommand : CommerceCommand
    {
        private IRemoveWishListLinePipeline _pipeline;
        private readonly IFindEntityPipeline _getPipeline;

        public RemoveWishListLineCommand(IFindEntityPipeline getWishListPipeline, IRemoveWishListLinePipeline pipeline, IServiceProvider serviceProvider)
          : base(serviceProvider)
        {
            _getPipeline = getWishListPipeline;
            _pipeline = pipeline;
        }

        public virtual async Task<WishList> Process(CommerceContext commerceContext, string wishListId, string wishListLineId)
        {
            var line = new WishListLineComponent(wishListLineId);
            return await Process(commerceContext, wishListId, line);
        }

        public virtual async Task<WishList> Process(CommerceContext commerceContext, string wishListId, WishListLineComponent line)
        {
            WishList result = null;
            using (CommandActivity.Start(commerceContext, this))
            {
                var context = commerceContext.GetPipelineContextOptions();
                var findEntityArgument = new FindEntityArgument(typeof(WishList), wishListId);
                var wishList = await _getPipeline.Run(findEntityArgument, context) as WishList;
                if (wishList == null)
                {
                    await context.CommerceContext.AddMessage(commerceContext.GetPolicy<KnownResultCodes>().ValidationError, "EntityNotFound", new object[] {wishListId}, string.Format("Entity {0} was not found.", wishListId));
                    return null;
                }
                if (wishList.Lines.FirstOrDefault<WishListLineComponent>(c => c.Id == line.Id) == null)
                {
                    await context.CommerceContext.AddMessage(commerceContext.GetPolicy<KnownResultCodes>().ValidationError, "WishListLineNotFound", new object[] {line.Id}, string.Format("WishList line {0} was not found", line.Id));
                    return wishList;
                }
                result = await _pipeline.Run(new WishListLineArgument(wishList, line), context);
                return result;
            }
        }

        public virtual async Task<WishList> Process(CommerceContext commerceContext, WishList wishList, WishListLineComponent line)
        {
            WishList result = null;
            using (CommandActivity.Start(commerceContext, this))
            {
                var context = commerceContext.GetPipelineContextOptions();
                result = await _pipeline.Run(new WishListLineArgument(wishList, line), context);
            }
            return result;
        }
    }
}
