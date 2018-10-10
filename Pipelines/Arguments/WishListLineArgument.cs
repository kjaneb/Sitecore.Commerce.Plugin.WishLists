using Sitecore.Framework.Conditions;

namespace Sitecore.Commerce.Plugin.WishLists
{
    public class WishListLineArgument : WishListArgument
    {
        public WishListLineArgument(WishList wishList, WishListLineComponent line) : base(wishList)
        {
            Condition.Requires(line).IsNotNull("The line can not be null");
            WishList = wishList;
            Line = line;
        }

        public WishListLineComponent Line { get; set; }
    }
}
