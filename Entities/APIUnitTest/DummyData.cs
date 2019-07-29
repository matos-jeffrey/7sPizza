using SevensPizzaAPI.Model;
using SevensPizzaEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIUnitTest
{
    internal class DummyData
    {
        internal List<Pizza> GetListOfPizza()
        {
            List<Pizza> pizza = new List<Pizza>();
            pizza.Add(new Pizza() { PizzaID = 1 });
            pizza.Add(new Pizza() { PizzaID = 2 });

            return pizza;
        }

        internal Customer GetCustomer()
        {
            Customer cust = new Customer()
            {
                CustID = 1
            };

            return cust;
        }

        internal Order GetOrder()
        {
            Order order = new Order()
            {
                OrderID = 1,
                TotalPizza=1
            };

            return order;
        }

        internal List<Order> GetOrderList()
        {
            List<Order> order = new List<Order>();
            order.Add(new Order() { OrderID = 1, TotalPizza = 1 });
            order.Add(new Order() { OrderID = 2, TotalPizza = 1 });

            return order;
        }

        internal Pizza GetPizza()
        {
            Pizza pizza = new Pizza()
            {
                OrderID = 1,
                PizzaID = 1,
                Toppings="",
                Meats = GetMeatsList(),
                Veggies = GetVeggiesList()
                
            };

            return pizza;
        }

        internal List<Topping> GetMeatsList()
        {
            List<Topping> list = new List<Topping>();
            list.Add(new Topping() { Name = "Becon", Price = 2 });
            list.Add(new Topping() { Name = "MeatBall", Price = 2 });
            list.Add(new Topping() { Name = "Beff", Price = 2 });

            return list;
        }

        internal List<Topping> GetVeggiesList()
        {
            List<Topping> list = new List<Topping>();
            list.Add(new Topping() { Name = "Tomota", Price = 2 });
            list.Add(new Topping() { Name = "Pineapple", Price = 2 });
            list.Add(new Topping() { Name = "Onions", Price = 2 });

            return list;
        }

        internal List<Topping> GetToppings()
        {
            List<Topping> list = new List<Topping>();
            list.Add(new Topping() { Name = "Becon", Price = 2 });
            list.Add(new Topping() { Name = "MeatBall", Price = 2 });
            list.Add(new Topping() { Name = "Beff", Price = 2 });
            list.Add(new Topping() { Name = "Tomota", Price = 2 });
            list.Add(new Topping() { Name = "Pineapple", Price = 2 });
            list.Add(new Topping() { Name = "Onions", Price = 2 });

            return list;
        }

        internal QuantityUpdate GetQuantityUpdate()
        {
            QuantityUpdate update = new QuantityUpdate()
            {
                pid = 1,
                oid = 1,
                quantity = 2
            };
            return update;
        }

        internal PizzaAndOrder GetPizzaAndOrder()
        {
            PizzaAndOrder res = new PizzaAndOrder()
            {
                PizzaPrice = 24.00m,
                OrderTotalPrice = 24.00m,
                OrderTotalQuantity = 2
            };

            return res;
        }
    }
}
