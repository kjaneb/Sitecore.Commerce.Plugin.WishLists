using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.OData;
using Microsoft.AspNetCore.Mvc;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Core.Commands;

namespace Sitecore.Commerce.Plugin.WishLists
{
    public class CommandsController : CommerceController
    {
        public CommandsController(IServiceProvider serviceProvider, CommerceEnvironment globalEnvironment)
          : base(serviceProvider, globalEnvironment)
        {
        }

        [HttpPut]
        [Route("RemoveWishListLine()")]
        public async Task<IActionResult> RemoveWishListLine([FromBody] ODataActionParameters value)
        {
            if (!ModelState.IsValid || value == null)
                return new BadRequestObjectResult(ModelState);
            if (!value.ContainsKey("wishListId") || string.IsNullOrEmpty(value["wishListId"]?.ToString()) || !value.ContainsKey("wishListLineId") || string.IsNullOrEmpty(value["wishListLineId"]?.ToString()))
                return new BadRequestObjectResult(value);
            var wishListId = value["wishListId"].ToString();
            var wishListLineId = value["wishListLineId"].ToString();
            var command = Command<RemoveWishListLineCommand>();
            var wishList = await command.Process(CurrentContext, wishListId, wishListLineId);
            return new ObjectResult(command);
        }

        [HttpPut]
        [Route("AddWishListLine()")]
        public async Task<IActionResult> AddWishListLine([FromBody] ODataActionParameters value)
        {
            if (!ModelState.IsValid || value == null)
                return new BadRequestObjectResult(ModelState);
            if (!value.ContainsKey("wishListId") || string.IsNullOrEmpty(value["wishListId"]?.ToString()) || !value.ContainsKey("itemId") || string.IsNullOrEmpty(value["itemId"]?.ToString()))
                return new BadRequestObjectResult(value);
            var wishListId = value["wishListId"].ToString();
            var str = value["itemId"].ToString();
            var command = Command<AddWishListLineCommand>();
            var line = new WishListLineComponent()
            {
                ItemId = str
            };
            var wishList = await command.Process(CurrentContext, wishListId, line);
            return new ObjectResult(command);
        }
    }
}
