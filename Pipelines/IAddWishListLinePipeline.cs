using Sitecore.Commerce.Core;
using Sitecore.Framework.Pipelines;

namespace Sitecore.Commerce.Plugin.WishLists
{
    [PipelineDisplayName("pipelines:addwishlistline")]
    public interface IAddWishListLinePipeline : IPipeline<WishListLineArgument, WishList, CommercePipelineExecutionContext>
    {
    }
}
