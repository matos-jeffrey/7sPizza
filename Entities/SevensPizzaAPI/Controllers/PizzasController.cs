using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SevensPizzaAPI.DAL;
using SevensPizzaAPI.Model;
using SevensPizzaEntity;

namespace SevensPizzaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class PizzasController : ControllerBase
    {
        private readonly IPizza DAL;

        public PizzasController(IPizza dal)
        {
            DAL = dal;
        }

        // GET: api/Pizzas
        [HttpGet]
        public async Task<IEnumerable<Pizza>> GetPizza()
        {
            return await DAL.GetPizzaList();
        }

        // GET: api/Pizzas/1/5
        // edit pizza
        [HttpGet("{cid}/{pid}")]
        public async Task<IActionResult> GetPizza([FromRoute] int cid, [FromRoute] int pid)
        {
            var order = await DAL.GetOrderByCust(cid);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var pizza = await DAL.GetPizza(pid);
            if (pizza == null)
            {
                return NotFound();
            }
            if (order.OrderID != pizza.OrderID)
            {
                return BadRequest();
            }

           


            return Ok(pizza);
        }

        // POST: api/Pizzas
        // create new pizza
        //id is customer id
        [HttpPost("{id}")]
        public async Task<IActionResult> PostPizza([FromRoute] int id, [FromBody] Pizza pizza)
        {
            //check if customer exist
            var cust =await DAL.GetCustomer(id);
            if (cust == null)
            {
                return BadRequest();
            }
            //looking for the order of customer
            var order = await DAL.GetOrderByCust(id);
            if (order == null)
            {
                //create new order
                order = await DAL.CreateNewOrder(id);
            }
            pizza.OrderID = order.OrderID;
            //calculate the total price of pizza
            pizza.Price = TotalPrice(pizza);
            //save topping to string
            pizza.Toppings = ToppingToString(pizza);

            //update information 
            order.TotalPizza += pizza.Quantity;
            order.Price += pizza.Price;
            await DAL.CreateNewPizza(pizza);
            await DAL.UpdateOrder(order);
            return Ok();
        }

        

        //Put : api/Pizzas/1
        // quantity is old quantity
        //change from the custom page
        [HttpPut("{quantity}")]
        public async Task<IActionResult> PutPizza([FromRoute] int quantity, [FromBody] Pizza pizza)
        {
            //get the corresponing order
            var order = await DAL.GetOrder((int)pizza.OrderID);
            if (order == null)
            {
                return BadRequest();
            }
            //save temp price 
            var tempPrice = pizza.Price;
            //update  price  and ToppingList of pizza
            pizza.Price = TotalPrice(pizza);
            pizza.Toppings = ToppingToString(pizza);
            //update the quantity and price in order
            order.Price = order.Price - tempPrice + pizza.Price;
            order.TotalPizza = order.TotalPizza - quantity + pizza.Quantity;
            //update both order and pizza detail
            await DAL.UpdateOrder(order);
            await DAL.UpdatePizza(pizza);

            return Ok();


        }

        //Put:api/Pizzas/Change
        //change the pizza quantity from the shoppingCart page
        [HttpPut("Change")]
        public async Task<IActionResult> ChangeQuantity([FromBody]QuantityUpdate quantity)
        {
            //get the  order
            var order = await DAL.GetOrder(quantity.oid);
            if (order == null)
            {
                return BadRequest();
            }
            //get pizza
            var pizza = await DAL.GetPizza(quantity.pid);
            if (pizza == null)
            {
                return BadRequest();
            }

            GetToppings(pizza);
            var tempPrice = pizza.Price;
            var oldQuantity = pizza.Quantity;
            //update new quantity and the price of pizza;
            pizza.Quantity = quantity.quantity;

            pizza.Price = TotalPrice(pizza);
            order.Price = order.Price - tempPrice + pizza.Price;
            order.TotalPizza = order.TotalPizza - oldQuantity + pizza.Quantity;
            PizzaAndOrder update = new PizzaAndOrder()
            {
                PizzaPrice = pizza.Price,
                OrderTotalQuantity = order.TotalPizza,
                OrderTotalPrice = order.Price
            };
            //update both order and pizza detail
            await DAL.UpdateOrder(order);
            await DAL.UpdatePizza(pizza);
            return Ok(update);
        }

        // DELETE: api/Pizzas/1
        [HttpDelete("{pid}")]
        public async Task<IActionResult> DeletePizza([FromRoute] int pid)
        {
           if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pizza = await DAL.GetPizza(pid);
            if (pizza == null)
            {
                return NotFound();
            }

            await DAL.RemovePizza(pizza);

            return Ok();
        }
        private string ToppingToString(Pizza pizza)
        {
            StringBuilder str = new StringBuilder();
            foreach (var item in pizza.Meats)
            {
                if (item.IsSelected == true)
                    str.Append(item.Name + ",");
            }
            foreach (var item in pizza.Veggies)
            {
                if (item.IsSelected == true)
                    str.Append(item.Name + ",");
            }
            string result;
            if (str.Length == 0)
                result = "";
            else
                //save value to Toppings 
                result = str.ToString().Substring(0, (str.Length > 0) ? str.Length - 1 : 0);

            return result;
        }
        private decimal TotalPrice(Pizza pizza)
        {
            decimal Price = 0.0m;
            //check what size is the pizza
            //small :$8
            //medium:$12
            //large :$16
            if (pizza.Size == "Small")
                Price = 8;
            else if (pizza.Size == "Medium")
                Price = 12;
            else
                Price = 16;

            //base on topping
            foreach (var item in pizza.Meats)
            {
                if (item.IsSelected)
                    Price += item.Price;
            }

            foreach (var item in pizza.Veggies)
            {
                if (item.IsSelected)
                    Price += item.Price;
            }

            Price *= pizza.Quantity;

            return Price;
        }

        private void GetToppings(Pizza pizza)
        {
            var toppingList = DAL.GetToppings();
            var topping = pizza.Toppings.Split(",").ToList();
            //change isSelected for selected topping
            if (topping[0] !="")
            {
                //allow for pizza with topping
                foreach (var item in topping)
                {
                    var result = toppingList.Find(x => x.Name == item);
                    result.IsSelected = true;
                }
            }

            //separate to two list
            var meatList = toppingList.Where(x => x.ToppingType == "Meat").ToList();
            var veggiesList = toppingList.Where(x => x.ToppingType == "Veggies").ToList();

            //add to the pizaa
            pizza.Meats = meatList;
            pizza.Veggies = veggiesList;
        }

    }
}