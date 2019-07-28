using Microsoft.EntityFrameworkCore;
using SevensPizzaEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SevensPizzaAPI.DAL
{
    public class PizzaDAL : IPizza
    {
        private readonly SevensDBContext _context;

        public PizzaDAL(SevensDBContext context)
        {
            _context = context;
        }
        //check the customer is exist
        public async Task<Customer> GetCustomer(int id)
        {
            return await _context.Customer.Where(x => x.CustID == id).FirstAsync();
        }
        #region pizza
        //create new pizza
        public async Task<Pizza> CreateNewPizza(Pizza pizza)
        {
            _context.Pizza.Add(pizza);

            await _context.SaveChangesAsync();

            return pizza;
        }
        public async Task<Pizza> GetPizza(int pid)
        {
            return await _context.Pizza.Where(x => x.PizzaID == pid).FirstAsync();

        }
        //get the list of pizza
        public async Task<List<Pizza>> GetPizzaList()
        {
            return await _context.Pizza.ToListAsync();
        }
        public async Task<bool> UpdatePizza(Pizza pizza)
        {
            _context.Update(pizza);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemovePizza(Pizza pizza)
        {
            _context.Pizza.Remove(pizza);
            await _context.SaveChangesAsync();
            return true;
        }
        #endregion

        #region order
        public async Task<Order> GetOrder(int id)
        {
            return await _context.Order.Where(x => x.OrderID == id && x.Checkout ==false).FirstOrDefaultAsync();
        }
        public async Task<Order> GetOrderByCust(int id)
        {
            return await _context.Order.Where(x => x.CustID == id && x.Checkout == false).FirstOrDefaultAsync();
        }

        public async Task<List<Order>> GetOrderList()
        {
            return await _context.Order.ToListAsync();
        }
        public async Task<Order> GetOrderWithPizza(int id)
        {
            return await _context.Order.Include("PizzaList").Where(x => x.CustID == id && x.Checkout ==false).FirstOrDefaultAsync();
        }
        public async Task<Order> CreateNewOrder(int id)
        {
            //create new order
            Order order = new Order()
            {
                CustID = id,
                OrderTime = DateTime.Now
            };
            //save to database
            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            return order;
        }
        //chekcout method
        public async Task<bool> UpdateOrder(Order order)
        {

            _context.Update(order);
            await _context.SaveChangesAsync();
            return true;
        }
        #endregion

        #region Toppings
        public List<Topping> GetToppings()
        {
            return  _context.Topping.ToList();
        }
        #endregion

        #region credit card
        public async Task<int> AddCreditCard(int custId,CreditCard card)
        {
            //first check if the card already exist
            var exist = _context.CreditCard.Where(x => x.CardNumber == card.CardNumber && x.CecCode == card.CecCode && x.CustID == custId).FirstOrDefault();
            if(exist == null)
            {
                card.CustID = custId;
                //add to database
                _context.CreditCard.Add(card);
                await _context.SaveChangesAsync();
                return card.CardID;
            }
            
            return exist.CardID;
        }


        #endregion
    }
}
