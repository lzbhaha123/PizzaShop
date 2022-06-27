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
    public class OrdersController : Controller
    {
        private readonly PizzaShopContext _context;

        public OrdersController(PizzaShopContext context)
        {
            _context = context;
        }
      
        public IActionResult Add()
        {
            string details = HttpContext.Request.Form["order_detail"];
            string name = HttpContext.Request.Form["order_name"];
            string phone = HttpContext.Request.Form["order_phone"];
            int oid = new Random().Next(0,1000000);
            _context.Add(new Order(oid, name, phone, "", 1));
            _context.SaveChanges();
            details = details.Substring(0,details.Length-1);
            string[] list = details.Split(":");
            for(int i = 0; i < list.Length; i++)
            {
                int opid = _context.OrderPizza.Count() + 1;
                string[] each = list[i].Split(",");
                //int o = _context.Order.OrderBy(o => o.OrderId).Last().OrderId;
                //Order o = _context.Order.First(m => m.OrderId == 1);
                //Pizza p = _context.Pizza.First(m => m.PizzaId == Int32.Parse(each[0])); //_context.Pizza.FirstOrDefaultAsync(m => m.PizzaId == Int32.Parse(each[0]));
                _context.Add(new OrderPizza(_context.OrderPizza.Count()+1, oid, Int32.Parse(each[0]), Int32.Parse(each[1])));
                _context.SaveChanges();
            }

            //return Content(oid + "");

            ViewData["oid"] = oid;

            return View();
        }

        // GET: Admin/Orders
        public async Task<IActionResult> Index()
        {
            string username = "";
            if (HttpContext.Session.GetString("username") != "" && HttpContext.Session.GetString("username") != null)
            {
                username = HttpContext.Session.GetString("username");
                ViewData["username"] = username;
                return _context.Order != null ?
                          View(await _context.Order.ToListAsync()) :
                          Problem("Entity set 'PizzaShopContext.Order'  is null.");
            }
            else
            {
                return Redirect("/admin");
            }            
        }

        // GET: Admin/Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Admin/Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,ClientName,ClientPhone,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Admin/Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Admin/Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,ClientName,ClientPhone,Status")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Admin/Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Admin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Order == null)
            {
                return Problem("Entity set 'PizzaShopContext.Order'  is null.");
            }
            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                _context.Order.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return (_context.Order?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
