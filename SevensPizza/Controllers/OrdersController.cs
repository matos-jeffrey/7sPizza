using System;
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
    public class OrdersController : Controller
    {
        private OrderApi _api;
        public OrdersController(OrderApi api = null)
        {
            if (_api == null)
                _api = new OrderApi();
            else
                _api = api;
        }

        public async Task<IActionResult> ShoppingCart(int? id)
        {
            //hard code the customer id
            var custId = 1;
            var order = await _api.GetOrder(custId);

            Cart cart = new Cart()
            {
                OrderDetail = order,
                PizzaProperty = new Pizza()
            };
            return View(cart);
        }


        public async Task<IActionResult> Checkout(int? id)
        {
            //hard code the customer id
            var custId = 1;

            var order = await _api.GetOrder(custId);
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(Order order)
        {

            //hard code customer id
            var custId = 1;
            await _api.Checkout(custId,order);

            return View(order);
        }


    }
}
