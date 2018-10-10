using System.ComponentModel.DataAnnotations;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Conditions;

namespace Sitecore.Commerce.Plugin.WishLists
{
    //Class used to provide informaton to the GetWishListPipeline about the WishList to load.
    public class ResolveWishListArgument : PipelineArgument
    {
        public ResolveWishListArgument(string shopName, string wishListId, string shopperId)
        {
            Condition.Requires<string>(wishListId).IsNotNullOrEmpty("The wish list id can not be null or empty");
            ShopName = shopName;
            WishListId = wishListId;
            ShopperId = shopperId;
        }

        [StringLength(50)]
        public string ShopName { get; set; }

        public string ShopperId { get; set; }

        public string WishListId { get; set; }
    }
}
