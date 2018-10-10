using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Commerce.Core;

namespace Sitecore.Commerce.Plugin.WishLists
{
    public class WishListLineAdded : Model
    {
        public WishListLineAdded() : this(string.Empty)
        {
        }

        public WishListLineAdded(string lineId)
        {
            LineId = lineId;
        }

        public string LineId { get; set; }

    }
}
