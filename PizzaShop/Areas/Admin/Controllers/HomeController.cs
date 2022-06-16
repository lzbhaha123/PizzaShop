using Microsoft.AspNetCore.Mvc;
using PizzaShop.Data;
using PizzaShop.Models;
using System.Diagnostics;
using System.Linq;

namespace PizzaShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly PizzaShopContext _context;

        public HomeController(PizzaShopContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            string username = HttpContext.Session.GetString("username");
            if (username == ""|| username==null) { 
            }
            return View();
        }
        [HttpPost]
        public IActionResult Login()
        {
            string username = HttpContext.Request.Form["username"];
            string password = HttpContext.Request.Form["password"];
            List<User> users = _context.User.Where(u => u.Username == username && u.Password == password).ToList();
            if(users.Count != 0)
            {
                HttpContext.Session.SetString("username", username);
            }
            return Redirect("/Admin/Home");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}