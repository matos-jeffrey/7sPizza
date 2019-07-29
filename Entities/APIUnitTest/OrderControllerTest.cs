using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SevensPizzaAPI.Controllers;
using SevensPizzaEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIUnitTest
{
    [TestClass]
    public class OrderControllerTest
    {
        static OrdersController order;
        DummyData data = new DummyData();

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            TestDAL DAL = new TestDAL();
            order = new OrdersController(DAL);

        }

        [TestMethod]
        public async Task GetListOforder()
        {
            //call method
            var actual = await order.GetOrder();
            //the expected result value
            var expected = data.GetOrderList();
            //test the length of list
            Assert.AreEqual(2, actual.Count());
            
        }

        [TestMethod]
        public async Task ShoppingCart()
        {
            //call method
            var actual = await order.GetOrder(1);
            //the expected result value
            var expected = data.GetOrder();
            //test the length of list
            var result = actual as OkObjectResult;
            var value = result.Value as Order;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(expected.OrderID, value.OrderID);
        }


        [TestMethod]
        public async Task Checkout()
        {
            var checkout = data.GetOrder();
            //call method
            var actual = await order.Checkout(1,checkout);

            //test the length of list
            var result = actual as OkResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

        }



    }
}
