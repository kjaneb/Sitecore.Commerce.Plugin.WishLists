using Sitecore.Commerce.Core;
using Sitecore.Framework.Pipelines;

namespace Sitecore.Commerce.Plugin.WishLists
{
    [PipelineDisplayName("pipelines:removewishlistline")]
    public interface IRemoveWishListLinePipeline : IPipeline<WishListLineArgument, WishList, CommercePipelineExecutionContext>
    {
    }
}
