using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Pipelines;

namespace Sitecore.Commerce.Plugin.WishLists
{
    [PipelineDisplayName("pipelines:convertwishlisttocart")]
    public interface IConvertWishListToCartPipeline : IPipeline<WishList, WishList, CommercePipelineExecutionContext>
    {
    }
}
