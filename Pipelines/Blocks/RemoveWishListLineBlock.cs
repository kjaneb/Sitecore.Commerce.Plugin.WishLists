using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;

namespace Sitecore.Commerce.Plugin.WishLists
{
    [PipelineDisplayName("WishLists.RemoveWishListLineBlock")]
    public class RemoveWishListLineBlock : PipelineBlock<WishListLineArgument, WishList, CommercePipelineExecutionContext>
    {
        public override async Task<WishList> Run(WishListLineArgument arg, CommercePipelineExecutionContext context)
        {
            Condition.Requires(arg).IsNotNull("The argument can not be null");
            Condition.Requires(arg.WishList).IsNotNull("The wish list can not be null");
            Condition.Requires(arg.Line).IsNotNull("The lines can not be null");
            var wishList = arg.WishList;
            var lines = wishList.Lines.ToList();
            var existingLine = lines.FirstOrDefault(l => l.Id.Equals(arg.Line.Id, StringComparison.OrdinalIgnoreCase));
            if (existingLine != null)
            {
                await context.CommerceContext.AddMessage(context.GetPolicy<KnownResultCodes>().Information, null, null, string.Format("Removed Line '{0}' from WishList '{1}'.", existingLine.Id, wishList.Id));
                lines.Remove(existingLine);
            }
            wishList.Lines = lines;
            return wishList;
        }
    }
}
