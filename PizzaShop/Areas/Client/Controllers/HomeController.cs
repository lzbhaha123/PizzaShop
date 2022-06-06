using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Data;
using PizzaShop.Models;


namespace PizzaShop.Areas.Client.Controllers
{
    [Area("Client")]
    public class HomeController : Controller
    {
        private readonly PizzaShopContext _context;
        public HomeController(PizzaShopContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["pizzas"] = _context.Pizza.ToList();

            //var pizza = await _context.Pizza.ToListAsync();

            return View();
        }

    }
}