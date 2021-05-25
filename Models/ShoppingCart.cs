using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CakeShop.Models
{
    public class ShoppingCart
    {
        private readonly AppDbContext _appDbContext;
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> shoppingCartItems { get; set; }
        public ShoppingCart(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            var context = services.GetService<AppDbContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);
            return new ShoppingCart(context) { ShoppingCartId = cartId };
           
        }
        public void AddToCart(Cake cake, int amount)
        {
            var shoppingCartItem = _appDbContext.shoppingCartItems.SingleOrDefault(
                s => s.cake.CakeId == cake.CakeId && s.ShoppingCartId == ShoppingCartId);
            if(shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    cake = cake,
                    Amount = 1
                };
                _appDbContext.shoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _appDbContext.SaveChanges();
        }
        public int RemoveFromCart(Cake cake)
        {
            var shoppingCartItem = _appDbContext.shoppingCartItems.SingleOrDefault(
                s => s.cake.CakeId == cake.CakeId && s.ShoppingCartId == ShoppingCartId);

            var localamount = 0;
            if(shoppingCartItem != null)
            {
                if(shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localamount = shoppingCartItem.Amount;
                }
                else
                {
                    _appDbContext.shoppingCartItems.Remove(shoppingCartItem);
                }
            }
            _appDbContext.SaveChanges();
            return localamount;
        }
        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return shoppingCartItems ?? (shoppingCartItems =
                _appDbContext.shoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId).
                Include(s => s.cake).ToList());
        }
        public void ClearCart()
        {
            var cartItems = _appDbContext
                .shoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);
            _appDbContext.shoppingCartItems.RemoveRange(cartItems);
            _appDbContext.SaveChanges();
        }
        public decimal GetShoppingCartTotal()
        {
            var total = _appDbContext.shoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.cake.Price * c.Amount).Sum();
            return total;
        }
    }
}
