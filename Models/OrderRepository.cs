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

            order.OrderDetails = new List<OrderDetail>();
            var shoppingCartItems = _shoppingCart.shoppingCartItems;
            decimal unitPrice = 0;
            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = shoppingCartItem.Amount,
                    CakeId = shoppingCartItem.cake.CakeId,
                    Price = shoppingCartItem.cake.Price

                };
                unitPrice = shoppingCartItem.cake.Price + unitPrice;

                order.OrderDetails.Add(orderDetail);


            }
            order.OrderTotal = unitPrice;
            _appDbContext.Order.Add(order);
            _appDbContext.SaveChanges();
        }
    }
}
