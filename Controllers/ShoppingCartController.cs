using CakeShop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CakeShop.ViewModels;

namespace CakeShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ICakeRepository _cakeRepository;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(ICakeRepository cakeRepository, ShoppingCart shoppingCart)
        {
            _cakeRepository = cakeRepository;
            _shoppingCart = shoppingCart;
        }
        public IActionResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.shoppingCartItems = items;
            var ShoppingCartViewModel = new ShoppingCartViewModel()
            {
                shoppingCart = _shoppingCart,
                ShoppinCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(ShoppingCartViewModel);
        }
        public RedirectToActionResult AddToShoppingCart(int cakeId)
        {
            var selectedCake = _cakeRepository.AllCakes.FirstOrDefault(c => c.CakeId == cakeId);
            if(selectedCake != null)
            {
                _shoppingCart.AddToCart(selectedCake, 1);
            }
           
            return RedirectToAction("Index");
        }
        public RedirectToActionResult RemoveFromCart(int cakeId)
        {
            var selectedCake = _cakeRepository.AllCakes.FirstOrDefault(c => c.CakeId == cakeId);
           
            if(selectedCake != null)
            {
                _shoppingCart.RemoveFromCart(selectedCake);
            }
            return RedirectToAction("Index");
        }
    }
}
