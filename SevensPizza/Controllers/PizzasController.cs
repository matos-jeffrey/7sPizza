using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SevensPizzaEntity;

namespace SevensPizza.Controllers
{
    public class PizzasController : Controller
    {
        private readonly SevensDBContext _context;
        HttpClient client = new HttpClient();

        public PizzasController(SevensDBContext context)
        {
            _context = context;
        }

        // GET: Pizzas
        public async Task<IActionResult> Index()
        {
            var sevensDBContext = _context.Pizza.Include(p => p.order);
            return View(await sevensDBContext.ToListAsync());
        }

        // GET: Pizzas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizza = await _context.Pizza
                .Include(p => p.order)
                .FirstOrDefaultAsync(m => m.PizzaID == id);
            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        // GET: Pizzas/Create
        public IActionResult Create()
        {
            ViewData["OrderID"] = new SelectList(_context.Order, "OrderID", "PaymentType");
            return View();
        }

        // POST: Pizzas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PizzaID,PizzaName,Quantity,Size,CrustStyle,Sauce,SauceAmount,CheeseAmount,Price,OrderID,Toppings")] Pizza pizza)
        {
            if (ModelState.IsValid)
            {
                pizza.OrderID = 0;
                //client.BaseAddress = new Uri("https://7spizzaapi.azurewebsites.net/");
                client.BaseAddress = new Uri("http://localhost:64474/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                //HttpResponseMessage response = await client.PostAsJsonAsync("api/Customers", customer);
                var response = await client.PostAsync("api/Pizzas", new JsonContent(pizza));

                ViewData["Response"] = response; //response.EnsureSuccessStatusCode();

                // return URI of the created resource.
                return View(pizza);

                _context.Add(pizza);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderID"] = new SelectList(_context.Order, "OrderID", "PaymentType", pizza.OrderID);
            return View(pizza);
        }

        // GET: Pizzas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizza = await _context.Pizza.FindAsync(id);
            if (pizza == null)
            {
                return NotFound();
            }
            ViewData["OrderID"] = new SelectList(_context.Order, "OrderID", "PaymentType", pizza.OrderID);
            return View(pizza);
        }

        // POST: Pizzas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PizzaID,PizzaName,Quantity,Size,CrustStyle,Sauce,SauceAmount,CheeseAmount,Price,OrderID,Toppings")] Pizza pizza)
        {
            if (id != pizza.PizzaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pizza);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PizzaExists(pizza.PizzaID))
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
            ViewData["OrderID"] = new SelectList(_context.Order, "OrderID", "PaymentType", pizza.OrderID);
            return View(pizza);
        }

        // GET: Pizzas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizza = await _context.Pizza
                .Include(p => p.order)
                .FirstOrDefaultAsync(m => m.PizzaID == id);
            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        // POST: Pizzas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pizza = await _context.Pizza.FindAsync(id);
            _context.Pizza.Remove(pizza);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PizzaExists(int id)
        {
            return _context.Pizza.Any(e => e.PizzaID == id);
        }
    }
}
