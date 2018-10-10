using Sitecore.Commerce.Core;
using Sitecore.Framework.Conditions;

namespace Sitecore.Commerce.Plugin.WishLists
{
    public class WishListArgument : PipelineArgument
    {
        public WishListArgument(WishList wishList)
        {
            Condition.Requires(wishList).IsNotNull("The WishList cannot be null");
        }
        public WishList WishList { get; set; }
    }
}
