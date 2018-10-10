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
    [PipelineDisplayName("WishLists.AddWishListLineBlock")]
    public class AddWishListLineBlock : PipelineBlock<WishListLineArgument, WishList, CommercePipelineExecutionContext>
    {
        public override async Task<WishList> Run(WishListLineArgument arg, CommercePipelineExecutionContext context)
        {
            Condition.Requires(arg).IsNotNull(string.Format("{0}: The argument cannot be null.", Name));
            Condition.Requires(arg.WishList).IsNotNull(string.Format("{0}: The WishList cannot be null.", Name));
            Condition.Requires(arg.Line).IsNotNull(string.Format("{0}: The line to add cannot be null.", Name));

            context.CommerceContext.AddObject(arg);
            var wishList = arg.WishList;

            var existingLine = wishList.Lines.FirstOrDefault(l => l.ItemId.Equals(arg.Line.ItemId, StringComparison.Ordinal));

            if (!string.IsNullOrEmpty(arg.Line.ItemId))
            {
                if (arg.Line.ItemId.Split('|').Length >= 3)
                {
                    if (string.IsNullOrEmpty(arg.Line.Id))
                        arg.Line.Id = Guid.NewGuid().ToString("N");
                    var list = wishList.Lines.ToList();
                    if (existingLine == null)
                    {
                        list.Add(arg.Line);
                        context.CommerceContext.AddModel(new WishListLineAdded(arg.Line.Id));
                    }

                    wishList.Lines = list;
                    return wishList;
                }
            }

            var defaultMessage = string.Format("Expecting a CatalogId and a ProductId in the ItemId: {0}.", arg.Line.ItemId);

            context.Abort(await context.CommerceContext.AddMessage(context.GetPolicy<KnownResultCodes>().Error, "ItemIdIncorrectFormat" , new object[] { arg.Line.ItemId }, defaultMessage), context);
            
            return wishList;
        }
    }
}
