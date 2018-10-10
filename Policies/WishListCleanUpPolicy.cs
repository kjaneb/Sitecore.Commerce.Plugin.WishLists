using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Commerce.Core;

namespace Sitecore.Commerce.Plugin.WishLists
{
    public class WishListCleanUpPolicy : Policy
    {
        public WishListCleanUpPolicy()
        {
            CleanUpOldLists = false;
        }

        public bool CleanUpOldLists { get; set; }
    }
}
