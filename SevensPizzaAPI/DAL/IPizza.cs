using SevensPizzaEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SevensPizzaAPI.DAL
{
    public interface IPizza
    {
        Task<Customer> GetCustomer(int id);
        //pizza
        Task<Pizza> CreateNewPizza(Pizza pizza);
        Task<List<Pizza>> GetPizzaList();
        Task<Pizza> GetPizza(int pid);
        Task<bool> UpdatePizza(Pizza pizza);
        Task<bool> RemovePizza(Pizza pizza);
        //order
        Task<List<Order>> GetOrderList();
        Task<Order> GetOrder(int id);
        Task<Order> GetOrderByCust(int id);
        Task<Order> CreateNewOrder(int id);
        Task<bool> UpdateOrder(Order order);
        Task<Order> GetOrderWithPizza(int id);

        //Topping
        List<Topping> GetToppings();
    }
}
