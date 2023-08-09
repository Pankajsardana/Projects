using ePizzaHub.Core;
using ePizzaHub.Core.Entities;
using ePizzaHub.Models;
using ePizzaHub.Respository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Respository.Implementation
{
    public class CartRespository : Respository<Cart>, ICartRespository
    {
        
        public CartRespository(AppDbContext db):base(db) 
        { }
        
        public int DeleteItem(Guid id, int itemId)
        {
            var item =_db.CartItems.Where(ci=>ci.ItemId == itemId && ci.CartId ==id).FirstOrDefault();
            if(item != null)
            {
                _db.CartItems.Remove(item);
                return _db.SaveChanges();
            }
            return 0;
            //throw new NotImplementedException();
        }

        public Cart GetCart(Guid id)
        {
            return _db.Carts.Include(c=>c.CartItems).                
                Where(ci => ci.Id == id && ci.IsActive==true).FirstOrDefault();

            
            //throw new NotImplementedException();
        }

        public CartModel GetCartDetails(Guid id)
        {

            
            throw new NotImplementedException();
        }

        public int UpdateCart(Guid id, int userid)
        {
            Cart cart=GetCart(id);
            cart.UserId = userid;
            return _db.SaveChanges();

            //throw new NotImplementedException();
        }

        public int UpdateQuantity(Guid id, int quantity, int itemId)
        {
            bool flag = false;
            Cart cart=GetCart(id);
            if (cart != null)
            {
                var cartItems = cart.CartItems.ToList();
                for (int i = 0; i < cartItems.Count; i++)
                {
                    if (cartItems[i].Id == itemId)
                    {
                        cartItems[i].Quantity=+ quantity;
                        flag = true;
                        break;
                    }
                }

                if (flag)
                {
                    cart.CartItems = cartItems;
                    return _db.SaveChanges();
                }
            }
            return 0;
            //throw new NotImplementedException();
        }

       
    }
}
