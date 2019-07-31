using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SevensPizza.Models;
using SevensPizzaEntity;

namespace SevensPizza.Controllers
{
    public class PizzasController : Controller
    {

        HttpClient client = new HttpClient();

        private PizzaApi _api;
        public PizzasController(PizzaApi api = null)

        {
            if (_api == null)
                _api = new PizzaApi();
            else
                _api = api;
        }



        // GET: Pizzas/Create
        public IActionResult Create()
        {

            return View();
        }

        /*
         * custom pizza page, fetch list of topping
         */
        //Pizza/Custom
        public async Task<IActionResult> Custom(int? id)
        {
            var toppingList = await _api.GetTopping();
            Pizza pizza = null;
            if (id != null)
            {
                //handcode customer id =1;
                var custId = 1;
                //get the pizza from pizza table
                pizza = await _api.GetPizza((int)id,custId);
                //separate the string to list
                var topping = pizza.Toppings.Split(",").ToList();
                //change isSelected for selected topping
                if (topping[0] != "")
                {
                    //allow for pizza with topping
                    foreach (var item in topping)
                    {
                        var result = toppingList.Find(x => x.Name == item);
                        result.IsSelected = true;
                    }
                }
                if (id >= 32 && id <= 38)
                {
                    ViewData["type"] = "create";
                }
                else
                {
                    ViewData["type"] = "edit";
                }
            }
            else
            {
                pizza = new Pizza();
                ViewData["type"] = "create";
            }

            //separate to two list
            var meatList = toppingList.Where(x => x.ToppingType == "Meat").ToList();
            var veggiesList = toppingList.Where(x => x.ToppingType == "Veggie").ToList();

            //add to the pizaa
            pizza.Meats = meatList;
            pizza.Veggies = veggiesList;

            return View(pizza);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // Post: Pizzas/Custom
        public async Task<IActionResult> Custom(Pizza pizza)
        {
            pizza.OrderID = null;
            pizza.PizzaID = 0;
            //handcode customer id =1;
            var custId = 1;
            var success = await _api.PostPizza(custId,pizza);

            if (!success)
            {
                return NotFound();
            }
            return RedirectToAction("Menu", "Home");
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
            }
            return View();
        }
        //Post: Pizzas/Update
        public async Task<IActionResult> Update(int oldQuantity, Pizza pizza)
        {
            var success = await _api.UpdatePizza(oldQuantity, pizza);


            if (!success)
            {
                return NotFound();
            }
                return RedirectToAction("ShoppingCart", "Orders");
            
        }

    }
}
