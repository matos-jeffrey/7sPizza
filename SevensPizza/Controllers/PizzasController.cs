﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        private PizzaApi _api;
        public PizzasController(PizzaApi api = null)
        {
            if (_api == null)
                _api = new PizzaApi();
            else
                _api = api;
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
                if (topping.Count > 1)
                {
                    //allow for pizza with topping
                    foreach (var item in topping)
                    {
                        var result = toppingList.Find(x => x.Name == item);
                        result.IsSelected = true;
                    }
                }

                ViewData["type"] = "edit";
            }
            else
            {
                pizza = new Pizza();
                ViewData["type"] = "create";
            }

            //separate to two list
            var meatList = toppingList.Where(x => x.ToppingType == "Meat").ToList();
            var veggiesList = toppingList.Where(x => x.ToppingType == "Veggies").ToList();

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
            //handcode customer id =1;
            var custId = 1;
            var success = await _api.PostPizza(custId,pizza);

            if (!success)
            {
                return NotFound();
            }
            return RedirectToAction("Menu", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
