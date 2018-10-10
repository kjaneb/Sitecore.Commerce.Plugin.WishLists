using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Core.Commands;
using Sitecore.Framework.Pipelines;

namespace Sitecore.Commerce.Plugin.WishLists
{
    public class GetWishListCommand : CommerceCommand
    {
        private IGetWishListPipeline _pipeline;

        public GetWishListCommand(IGetWishListPipeline pipeline, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _pipeline = pipeline;
        }

        public virtual async Task<WishList> Process(CommerceContext commerceContext, string wishListId, bool secureResult = true)
        {
            using (CommandActivity.Start(commerceContext, this))
            {
                var context = commerceContext.GetPipelineContextOptions();
                var resolveWishListArgument = new ResolveWishListArgument(commerceContext.CurrentShopName(), wishListId, commerceContext.CurrentShopperId());
                var objects = commerceContext.GetObjects<WishList>();
                if (objects.Any(p => p.Id == wishListId))
                {
                    commerceContext.Logger.LogTrace(string.Format("GetWishListCommand.AlreadyLoaded: WishListId={0}", wishListId), Array.Empty<object>());
                    return objects.FirstOrDefault(p => p.Id == wishListId);
                }
                commerceContext.Logger.LogTrace(string.Format("GetWishListCommand.LoadingWishList: WishListId={0}", wishListId), Array.Empty<object>());
                var wishList = await _pipeline.Run(resolveWishListArgument, context);
                commerceContext.Logger.LogTrace(string.Format("GetWishListCommand.WishListLoaded: WishListId={0}", wishListId), Array.Empty<object>());

                return wishList;
            }
        }
    }
}
