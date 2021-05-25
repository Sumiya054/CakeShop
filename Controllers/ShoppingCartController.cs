using CakeShop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return View();
        }
    }
}
