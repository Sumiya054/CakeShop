﻿using CakeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CakeShop.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Cake> CakesOfTheWeek { get; set; }
    }
}
