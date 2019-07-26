using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SevensPizzaEntity;

namespace SevensPizza.Controllers
{
    public class Pizzas2Controller : Controller
    {
        private readonly SevensDBContext _context;

        public Pizzas2Controller(SevensDBContext context)
        {
            _context = context;
        }
        //Pizza/Custom
        public async Task<IActionResult> Custom(int? id)
        {
            List<Topping> list = new List<Topping>();
            list.Add(new Topping() { Name = "Chicken", Price = 10, IsSelected = true });
            list.Add(new Topping() { Name = "Beef", Price = 20 });
            list.Add(new Topping() { Name = "Pepperoni", Price = 30 });
            list.Add(new Topping() { Name = "Meatball", Price = 40 });
            list.Add(new Topping() { Name = "Bacon", Price = 60 });
            list.Add(new Topping() { Name = "Steak", Price = 70 });
            list.Add(new Topping() { Name = "Sausage", Price = 80 });
            list.Add(new Topping() { Name = "Anchovies", Price = 80 });
            list.Add(new Topping() { Name = "Salami", Price = 80 });


            List<Topping> viggies = new List<Topping>();
            viggies.Add(new Topping() { Name = "Mushrooms", Price = 10 });
            viggies.Add(new Topping() { Name = "Tomatoes", Price = 10, IsSelected = true });
            viggies.Add(new Topping() { Name = "Pineapple", Price = 10 });
            viggies.Add(new Topping() { Name = "Onions", Price = 10 });
            viggies.Add(new Topping() { Name = "Black Olives", Price = 10 });
            viggies.Add(new Topping() { Name = "Spinach", Price = 10 });
            viggies.Add(new Topping() { Name = "Jalapeño Peppers", Price = 10 });
            viggies.Add(new Topping() { Name = "Banana Peppers", Price = 10 });
            viggies.Add(new Topping() { Name = "Green Peppers", Price = 10 });
            viggies.Add(new Topping() { Name = "Red Peppers", Price = 10 });



            Pizza order = new Pizza()
            {
                Meats = list,
                Veggies = viggies
            };
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // GET: Orders/Custom
        public async Task<IActionResult> Custom(Pizza pizza)
        {
            Console.WriteLine(pizza.Meats);


            return View();
        }
    }
}
