using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;

namespace Sitecore.Commerce.Plugin.WishLists
{
    public class GetWishListBlock : PipelineBlock<ResolveWishListArgument, WishList, CommercePipelineExecutionContext>
    {
        private IFindEntityPipeline _findEntityPipeline;

        public GetWishListBlock(IFindEntityPipeline findEntityPipeline)
        {
            _findEntityPipeline = findEntityPipeline;
        }

        public override async Task<WishList> Run(ResolveWishListArgument arg, CommercePipelineExecutionContext context)
        {
            Condition.Requires(arg).IsNotNull("The argument can not be null");
            Condition.Requires(arg.WishListId).IsNotNullOrEmpty("The wishList id can not be null or empty");

            var objects = context.CommerceContext.GetObjects<WishList>();
            if (objects.Any(p => p.Id == arg.WishListId))
            {
                context.Logger.LogWarning(string.Format("{0}.AlreadyLoaded: WishListId:{1}", Name, arg.WishListId), Array.Empty<object>());
            }
            var wishList = await _findEntityPipeline.Run(new FindEntityArgument(typeof(WishList), arg.WishListId, true), context) as WishList;
            if (wishList == null || wishList.IsPersisted)
                return wishList;
            wishList.Id = arg.WishListId;
            wishList.Name = arg.WishListId;
            wishList.ShopName = arg.ShopName;

            return wishList;
        }
    }
}
