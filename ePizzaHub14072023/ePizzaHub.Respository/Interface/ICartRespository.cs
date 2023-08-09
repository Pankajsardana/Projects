using ePizzaHub.Core.Entities;
using ePizzaHub.Models;
using ePizzaHub.Respository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Respository.Interface
{
    public interface ICartRespository: IRespository<Cart>
    {
        Cart GetCart(Guid id);
        CartModel GetCartDetails(Guid id);

       
        int DeleteItem(Guid id,int itemId);

        int UpdateQuantity(Guid id,int quantity,int itemId);

        int UpdateCart(Guid id,int userid);


    }
}
