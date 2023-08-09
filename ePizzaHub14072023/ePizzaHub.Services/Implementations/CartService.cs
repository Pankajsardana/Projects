using ePizzaHub.Core;
using ePizzaHub.Core.Entities;
using ePizzaHub.Models;
using ePizzaHub.Respository.Interface;
using ePizzaHub.Services.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Services.Implementations
{
    public class CartService:Service<Cart>,ICartService
    {
        ICartRespository _cartRepo;
        IRespository<CartItem> _cartItem;
        IConfiguration _configuration;
        public CartService(IRespository<CartItem> cartItem,ICartRespository cartRepo,IConfiguration configuration):base(cartRepo)
        {
                _cartRepo = cartRepo;
                _cartItem = cartItem;
                _configuration = configuration;
        }

        public Cart AddItem(int userid, Guid guid, int itemid, decimal unitprice, int quantity)
        {
            Cart cart = _cartRepo.GetCart(guid);
            if(cart == null) 
            {
                cart = new Cart();
                CartItem cartItem = new CartItem { ItemId=itemid,Quantity=quantity,UnitPrice=unitprice,CartId=guid };

                cart.Id = guid;
                cart.UserId = userid;
                cart.CreatedDate=DateTime.Now;
                cart.IsActive = true;

                cart.CartItems.Add(cartItem);

                _cartRepo.Add(cart);
                _cartRepo.SaveChanges();
            }
            else 
            {
               CartItem cartItem = cart.CartItems.Where(c=>c.ItemId==itemid).FirstOrDefault();
                if (cartItem != null) 
                {
                    cartItem.Quantity += quantity;
                    _cartItem.Update(cartItem);
                    _cartItem.SaveChanges();
                }
                else 
                {
                    CartItem item = new CartItem { ItemId = itemid, Quantity = quantity, UnitPrice = unitprice, CartId = guid };
                    cart.CartItems.Add(item);
                    _cartItem.Update(item);
                    _cartItem.SaveChanges();
                }
            }
            return cart;
        }

        public int DeleteItem(Guid id, int itemId)
        {
            return _cartRepo.DeleteItem(id, itemId);
        }

        public int GetCartCount(Guid id)
        {
            var cart = _cartRepo.GetCart(id);
            return cart != null ? cart.CartItems.Count() : 0;

        }

        public CartModel GetCartDetails(Guid id)
        {
            var model=_cartRepo.GetCartDetails(id);
            if(model!=null && model.Items.Count > 0) 
            {
                decimal Subtotal = 0;
                foreach(var item in model.Items)
                {
                    item.Total=item.UnitPrice*item.Quantity;
                    Subtotal += item.Total;


                }
                model.Total = Subtotal;
                model.Tax =Math.Round((model.Total * Convert.ToInt32(_configuration["Tax:GST"]))/100,2);
                model.GrandTotal=model.Total+model.Tax;
            }
            return model;
        }

        public int UpdateCart(Guid id, int userid)
        {
            return _cartRepo.UpdateCart(id, userid); 
        }

        public int UpdateQuantity(Guid id, int quantity, int Id)
        {
            return _cartRepo.UpdateQuantity(id, quantity, Id);
        }
    }
}
