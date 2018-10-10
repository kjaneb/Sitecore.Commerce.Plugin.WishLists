using System.Threading.Tasks;
using Microsoft.AspNetCore.OData.Builder;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Core.Commands;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;

namespace Sitecore.Commerce.Plugin.WishLists
{
    [PipelineDisplayName("WishListConfigureServiceApiBlock")]
    public class ConfigureServiceApiBlock : PipelineBlock<ODataConventionModelBuilder, ODataConventionModelBuilder, CommercePipelineExecutionContext>
    {
        public override Task<ODataConventionModelBuilder> Run(ODataConventionModelBuilder modelBuilder, CommercePipelineExecutionContext context)
        {
            Condition.Requires(modelBuilder).IsNotNull($"{this.Name}: The argument cannot be null.");

            modelBuilder.AddEntityType(typeof(WishList));
            modelBuilder.AddComplexType(typeof(WishListLineAdded));
            modelBuilder.EntitySet<WishList>("WishLists");

            var addLineConfiguration = modelBuilder.Action("AddWishListLine");
            addLineConfiguration.Parameter<string>("wishListId");
            addLineConfiguration.Parameter<string>("itemId");
            addLineConfiguration.ReturnsFromEntitySet<CommerceCommand>("Commands");

            var removeLineConfiguration = modelBuilder.Action("RemoveWishListLine");
            removeLineConfiguration.Parameter<string>("wishListId");
            removeLineConfiguration.Parameter<string>("wishListLineId");
            removeLineConfiguration.ReturnsFromEntitySet<CommerceCommand>("Commands");

            return Task.FromResult(modelBuilder);
        }
    }
}
