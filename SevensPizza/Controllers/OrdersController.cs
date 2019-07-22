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
    public class OrdersController : Controller
    {
        private readonly SevensDBContext _context;

        public OrdersController(SevensDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ShoppingCart(int? id)
        {
            /*
             * 1st pizza
             */
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

            /*
             * 2rd pizza
             */
            List<Topping> plist = new List<Topping>();
            list.Add(new Topping() { Name = "Chicken", Price = 10, IsSelected = true });
            list.Add(new Topping() { Name = "Beef", Price = 20 });
            list.Add(new Topping() { Name = "Pepperoni", Price = 30, IsSelected = true });
            list.Add(new Topping() { Name = "Meatball", Price = 40 });
            list.Add(new Topping() { Name = "Bacon", Price = 60 });
            list.Add(new Topping() { Name = "Steak", Price = 70 });
            list.Add(new Topping() { Name = "Sausage", Price = 80 });
            list.Add(new Topping() { Name = "Anchovies", Price = 80 });
            list.Add(new Topping() { Name = "Salami", Price = 80 });

            List<Topping> pviggies = new List<Topping>();
            pviggies.Add(new Topping() { Name = "Mushrooms", Price = 10 });
            pviggies.Add(new Topping() { Name = "Tomatoes", Price = 10, IsSelected = true });
            pviggies.Add(new Topping() { Name = "Pineapple", Price = 10 });
            pviggies.Add(new Topping() { Name = "Onions", Price = 10 });
            pviggies.Add(new Topping() { Name = "Black Olives", Price = 10 });
            pviggies.Add(new Topping() { Name = "Spinach", Price = 10 });
            pviggies.Add(new Topping() { Name = "Jalapeño Peppers", Price = 10 });
            pviggies.Add(new Topping() { Name = "Banana Peppers", Price = 10 });
            pviggies.Add(new Topping() { Name = "Green Peppers", Price = 10 });
            pviggies.Add(new Topping() { Name = "Red Peppers", Price = 10 });
            List<Pizza> Plist = new List<Pizza>()
            {
                new Pizza()
                {
                    PizzaID=1,
                    Price=300,
                    Meats=list,
                    Veggies=viggies
                },
                new Pizza()
                {
                    PizzaID=2,
                    Price=200,
                    Meats=plist,
                    Veggies=pviggies
                }

            };
            Order order = new Order()
            {
                TotalPizza = 2,
                Price = 500,
                PizzaList = Plist
            };

            Cart cart = new Cart()
            {
                OrderDetail = order,
                PizzaProperty = new Pizza()
            };
            return View(cart);
        }


        public async Task<IActionResult> Checkout(int? id)
        {

            Order order = new Order()
            {
                TotalPizza = 2,
                Price = 500
            };

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout(Order order)
        {

            var date = order.Card.DOE;
          

            return View(order);
        }


    }
}
