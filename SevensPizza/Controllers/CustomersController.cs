using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SevensPizzaEntity;

namespace SevensPizza.Controllers
{
    public class CustomersController : Controller
    {
        private readonly SevensDBContext _context;
        //private string _url = "https://7spizzaapi.azurewebsites.net/api/Customers";
        HttpClient client = new HttpClient();

        public CustomersController(SevensDBContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customer.ToListAsync());
        }

        public IActionResult Login()
        {
            ViewData["URL"] = "http://localhost:64474/api/Customers";
            //ViewData["URL"] = "https://7spizzaapi.azurewebsites.net/api/Customers/";
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(string Email, string Password)
        //{
        //    if (Email == null || Password == null)
        //    {
        //        return View(); // RedirectToAction("ErrorPage", "MainMenu");
        //    }

        //    var customer = await _context.Customer
        //        .FirstOrDefaultAsync(m => m.Email == Email && m.Password == Password);
        //    if (customer == null)
        //    {
        //        return View(); // RedirectToAction("ErrorPage", "MainMenu");
        //    }

        //    return RedirectToAction("MainMenu", "MainMenu", new { customer.CustID });
        //}

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.CustID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustID,FirstName,LastName,Address,Email,Password,ConfirmPassword")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                client.BaseAddress = new Uri("https://7spizzaapi.azurewebsites.net/");
                //client.BaseAddress = new Uri("http://localhost:64474/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                //HttpResponseMessage response = await client.PostAsJsonAsync("api/Customers", customer);
                var response = await client.PostAsync("api/Customers", new JsonContent(customer));

                ViewData["Response"] = response; //response.EnsureSuccessStatusCode();

                // return URI of the created resource.
                return View(customer);

                //_context.Add(customer);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustID,FirstName,LastName,Address,Email,Password")] Customer customer)
        {
            if (id != customer.CustID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustID))
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
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.CustID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.CustID == id);
        }
    }

    public class JsonContent : StringContent
    {
        public JsonContent(object obj) :
            base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
        { }
    }
}
