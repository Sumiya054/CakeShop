using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CakeShop.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ShoppingCart _shoppingCart;

        public OrderRepository(AppDbContext appDbContext, ShoppingCart shoppingCart)
        {
            _appDbContext = appDbContext;
            _shoppingCart = shoppingCart;
        }
        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;
            _appDbContext.Order.Add(order);
            var shoppingCartItems = _shoppingCart.shoppingCartItems;
            foreach(var item in shoppingCartItems)
            {
                var orderDetails = new OrderDetail()
                {
                    Amount =item.Amount,
                    CakeId = item.cake.CakeId,
                    OrderId = order.OrderId,
                    Price = item.cake.Price
                };
                _appDbContext.OrderDetail.Add(orderDetails);
            }
            _appDbContext.SaveChanges();
        }
    }
}
