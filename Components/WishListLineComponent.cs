using System.Collections.Generic;
using Microsoft.AspNetCore.OData.Builder;
using Newtonsoft.Json;
using Sitecore.Commerce.Core;

namespace Sitecore.Commerce.Plugin.WishLists
{
    public class WishListLineComponent : Component
    {
        public WishListLineComponent()
        {

        }

        public WishListLineComponent(string lineId)
        {
            Id = lineId;
        }

        public string ItemId { get; set; }

        [Contained]
        [JsonIgnore]
        public IList<Component> WishListLineComponents { get; set; }
    }
}
