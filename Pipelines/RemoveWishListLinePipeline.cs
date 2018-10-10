using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Pipelines;

namespace Sitecore.Commerce.Plugin.WishLists
{
    public class RemoveWishListLinePipeline : CommercePipeline<WishListLineArgument, WishList>, IRemoveWishListLinePipeline
    {
        public RemoveWishListLinePipeline(IPipelineConfiguration configuration, ILoggerFactory loggerFactory) : base(configuration, loggerFactory)
        {
        }
    }
}
