using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SevensPizzaAPI.DAL;
using SevensPizzaEntity;

namespace SevensPizzaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class OrdersController : ControllerBase
    {
        private readonly IPizza DAL;


        public OrdersController(IPizza dal)
        {
            DAL = dal;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<IEnumerable<Order>> GetOrder()
        {
            return await DAL.GetOrderList();
        }

        // GET: api/Orders/5
        //id is customer Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //check if customer Id exist
            var cust =await DAL.GetCustomer(id);
            if (cust == null)
            {
                return BadRequest();
            }
            var order = await DAL.GetOrderWithPizza(id);
            //get the topping
            if (order == null)
            {
                //if customer doesn't have order before
                //or previous one is checkout
                //create new order
                order = await DAL.CreateNewOrder(id);
                order.PizzaList = new List<Pizza>();

            }

            return Ok(order);
        }


        //Put:api/Orders
        //id is customer id
        [HttpPut("{id}")]
        public async Task<IActionResult> Checkout([FromRoute] int id,[FromBody] Order order)
        {
            order.OrderTime = DateTime.Now;
            order.Checkout = true;
            if(order.Card != null)
            {
                //add to credit card table
                order.CardID=await DAL.AddCreditCard(id,order.Card);
            }
            //then update the order
            await DAL.UpdateOrder(order);
            return Ok();
        }

    }

        
}