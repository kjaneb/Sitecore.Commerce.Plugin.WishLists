using Sitecore.Commerce.Core;
using Sitecore.Framework.Pipelines;

namespace Sitecore.Commerce.Plugin.WishLists
{
    [PipelineDisplayName("pipelines:getwishlist")]
    public interface IGetWishListPipeline : IPipeline<ResolveWishListArgument, WishList, CommercePipelineExecutionContext>
    {
    }
}
