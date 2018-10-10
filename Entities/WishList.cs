using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.OData.Builder;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.Carts;
using Sitecore.Commerce.Plugin.ManagedLists;
using Sitecore.Commerce.Plugin.Pricing;

namespace Sitecore.Commerce.Plugin.WishLists
{
    public class WishList : CommerceEntity
    {
        public WishList()
        {
            Lines = new List<WishListLineComponent>();
            Components = new List<Component>();
        }

        public WishList(string id) : this()
        {
            Id = id;
        }

        [StringLength(50)]
        public string ShopName { get; set; }

        [Contained]
        public IList<WishListLineComponent> Lines { get; set; }
    }
}
