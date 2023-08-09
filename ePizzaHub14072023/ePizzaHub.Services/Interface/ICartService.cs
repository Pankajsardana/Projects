using ePizzaHub.Core.Entities;
using ePizzaHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Services.Interface
{
    public interface ICartService:IService<Cart>
    {
        int GetCartCount(Guid id);
        CartModel GetCartDetails(Guid id);

        Cart AddItem(int userid,Guid guid,int itemid,decimal unitprice,int quantity);

        int DeleteItem(Guid id, int itemId);

        int UpdateQuantity(Guid id, int quantity, int Id);

        int UpdateCart(Guid id, int userid);
    }
}
