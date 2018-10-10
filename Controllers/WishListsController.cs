using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.Logging;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sitecore.Commerce.Plugin.WishLists
{
    [EnableQuery]
    [Route("api/WishLists")]
    public class WishListsController : CommerceController
    {
        public WishListsController(IServiceProvider serviceProvider, CommerceEnvironment globalEnvironment) : base(serviceProvider, globalEnvironment)
        {
        }

        [HttpGet]
        [Route("(Id={id})")]
        [EnableQuery]
        public async Task<IActionResult> Get(string id)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(id))
                return NotFound();

            var wishList = await Command<GetWishListCommand>().Process(CurrentContext, id);
            return wishList != null ? new ObjectResult(wishList) : (IActionResult)NotFound();
        }
    }
}
