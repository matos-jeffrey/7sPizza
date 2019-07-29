using SevensPizzaAPI.DAL;
using SevensPizzaEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APIUnitTest
{
    public class TestDAL : IPizza
    {
        DummyData data = new DummyData();
        public Task<int> AddCreditCard(int custId, CreditCard card)
        {
            throw new NotImplementedException();
        }

        public Task<Order> CreateNewOrder(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Pizza> CreateNewPizza(Pizza pizza)
        {
            return data.GetPizza();
        }

        public async Task<Customer> GetCustomer(int id)
        {
            return data.GetCustomer();
        }

        public async Task<Order> GetOrder(int id)
        {
            if (id == 1)
            {
                //testing successful call
                return data.GetOrder();
            }
            //then testing failure 
            return null;
        }

        public async Task<Order> GetOrderByCust(int id)
        {
            if (id == 1)
            {
                //testing successful call
                return data.GetOrder();
            }
            //then testing failure 
            return null;
        }

        public async Task<List<Order>> GetOrderList()
        {
            return data.GetOrderList();
        }

        public async Task<Order> GetOrderWithPizza(int id)
        {
            return data.GetOrder();
        }

        public async Task<Pizza> GetPizza(int pid)
        {
            if (pid == 1)
                //testing success case
                return data.GetPizza();
            return null;
        }

        public async Task<List<Pizza>> GetPizzaList()
        {
            var pizza = data.GetListOfPizza();
            return pizza;
        }

        public  List<Topping> GetToppings()
        {
            return data.GetToppings();
        }

        public async Task<bool> RemovePizza(Pizza pizza)
        {
            return true;
        }

        public async Task<bool> UpdateOrder(Order order)
        {
            return true;
        }

        public async Task<bool> UpdatePizza(Pizza pizza)
        {
            return true;
        }
    }
}
