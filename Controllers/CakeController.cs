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
        public ViewResult List(string category)
        {
            IEnumerable<Cake> cakes;
            string currentCategory;
            if (string.IsNullOrEmpty(category))
            {
                cakes = _cakeRepository.AllCakes.OrderBy(c => c.CakeId);
                currentCategory = "All Cakes";
            }
            else
            {
                cakes = _cakeRepository.AllCakes.Where(c => c.Category.CategoryName == category).OrderBy(c => c.CakeId);
                currentCategory = _categoryRepository.AllCategories.FirstOrDefault(c => c.CategoryName == category)?.CategoryName;
            }
            
            return View(new CakesListViewModels { 
            Cakes = cakes,
            CurrentCategory = currentCategory
            });
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
