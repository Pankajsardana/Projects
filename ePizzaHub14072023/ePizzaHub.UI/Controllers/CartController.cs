using ePizzaHub.Core.Entities;
using ePizzaHub.Models;
using ePizzaHub.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ePizzaHub.UI.Controllers
{
    public class CartController : BaseController
    {
        ICartService _cartService;
        public CartController(ICartService cartService)
        {
                _cartService= cartService;
        }
        Guid CartId 
        {
            get 
            {
                Guid Id;
                string CId = Request.Cookies["CId"];
                if (string.IsNullOrEmpty(CId))
                {

                    Id = Guid.NewGuid();
                    Response.Cookies.Append("CID", Id.ToString(), new CookieOptions { Expires = DateTime.Now.AddDays(1) });

                }
                else 
                {
                    Id = Guid.Parse(CId);
                }
                return Id;


            }
        }

        [Route("Cart/AddToCart/{ItemId}/{UnitPrice}/{Quantity}")]
        public IActionResult AddToCart(int ItemId, int UnitPrice,int Quantity)
        {
            int userid =CurrentUser!=null ? CurrentUser.Id : 0;
            if(ItemId>0 && Quantity>0)
            {
                Cart cart =  _cartService.AddItem(userid,CartId, ItemId, UnitPrice, Quantity);
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler =ReferenceHandler.IgnoreCycles
                };
                var data = JsonSerializer.Serialize(cart, options);
                return Json(data);

            }
            return Json("");
        }
        public IActionResult Index()
        {

            CartModel cart = _cartService.GetCartDetails(CartId); 
            return View(cart);
        }
    }
}
