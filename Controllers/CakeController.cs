using CakeShop.Models;
using CakeShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CakeShop.Controllers
{
    public class CakeController : Controller
    {
        private readonly ICakeRepository _cakeRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CakeController(ICakeRepository cakeRepository, ICategoryRepository categoryRepository)
        {
            _cakeRepository = cakeRepository;
            _categoryRepository = categoryRepository;
        }
        public ViewResult List()
        {
            CakesListViewModels cakesListViewModels = new CakesListViewModels();
            cakesListViewModels.Cakes = _cakeRepository.AllCakes;
            cakesListViewModels.CurrentCategory = "Cheese Cake";
            return View(cakesListViewModels);
        }
        public IActionResult Details(int id)
        {
            var cake = _cakeRepository.GetCakeById(id);
            if(cake == null)
            {
                return NotFound();
            }
            return View(cake);
        }
    }
}
