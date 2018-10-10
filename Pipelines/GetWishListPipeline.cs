using Microsoft.Extensions.Logging;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Pipelines;

namespace Sitecore.Commerce.Plugin.WishLists
{
    public class GetWishListPipeline : CommercePipeline<ResolveWishListArgument, WishList>, IGetWishListPipeline
    {
        public GetWishListPipeline(IPipelineConfiguration<IGetWishListPipeline> configuration, ILoggerFactory loggerFactory) : base(configuration, loggerFactory)
        {
        }
    }
}
