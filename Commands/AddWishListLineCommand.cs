using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Core.Commands;
using Sitecore.Framework.Pipelines;

namespace Sitecore.Commerce.Plugin.WishLists
{
    public class AddWishListLineCommand : CommerceCommand
    {
        private readonly IAddWishListLinePipeline _addToWishListPipeline;
        private readonly IFindEntityPipeline _getPipeline;
        private readonly IPersistEntityPipeline _persistEntityPipeline;

        public AddWishListLineCommand(IFindEntityPipeline getWishListPipeline, IAddWishListLinePipeline addToWishListPipeline, IPersistEntityPipeline persistEntityPipeline, IServiceProvider serviceProvider)
          : base(serviceProvider)
        {
            _getPipeline = getWishListPipeline;
            _addToWishListPipeline = addToWishListPipeline;
            _persistEntityPipeline = persistEntityPipeline;
        }

        public virtual async Task<WishList> Process(CommerceContext commerceContext, string wishListId, WishListLineComponent line)
        {
            using (CommandActivity.Start(commerceContext, this))
            {
                var context = commerceContext.GetPipelineContextOptions();
                var findEntityArgument = new FindEntityArgument(typeof(WishList), wishListId, true);
                var wishList = await _getPipeline.Run(findEntityArgument, context) as WishList;
                if (wishList == null)
                {
                    await context.CommerceContext.AddMessage(commerceContext.GetPolicy<KnownResultCodes>().ValidationError, "EntityNotFound", new object[] { wishListId }, string.Format("Entity {0} was not found.", wishListId));
                    return null;
                }

                if (!wishList.IsPersisted)
                {
                    wishList.Id = wishListId;
                    wishList.Name = wishListId;
                    wishList.ShopName = commerceContext.CurrentShopName();
                }

                var result = await _addToWishListPipeline.Run(new WishListLineArgument(wishList, line), context);
                await _persistEntityPipeline.Run(new PersistEntityArgument(result), context);

                return result;
            }
        }

        public virtual async Task<WishList> Process(CommerceContext commerceContext, WishList wishList, WishListLineComponent line)
        {
            using (CommandActivity.Start(commerceContext, this))
            {
                var context = commerceContext.GetPipelineContextOptions();
                var arg = new WishListLineArgument(wishList, line);
                var result = await _addToWishListPipeline.Run(arg, context);
                return result;
            }
        }
    }
}
