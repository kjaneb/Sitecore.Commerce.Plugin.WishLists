using Microsoft.Extensions.Logging;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Pipelines;

namespace Sitecore.Commerce.Plugin.WishLists
{
    public class AddWishListLinePipeline : CommercePipeline<WishListLineArgument, WishList>, IAddWishListLinePipeline
    {
        public AddWishListLinePipeline(IPipelineConfiguration<IAddWishListLinePipeline> configuration, ILoggerFactory loggerFactory) : base(configuration, loggerFactory)
        {
        }
    }
}
