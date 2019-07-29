using Microsoft.VisualStudio.TestTools.UnitTesting;
using SevensPizzaAPI.Controllers;
using SevensPizzaEntity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SevensPizzaAPI.Model;

namespace APIUnitTest
{
    [TestClass]
    public class PizzaControllerTest
    {
        static PizzasController pizza;
        DummyData data = new DummyData();

        [ClassInitialize]
        public  static void ClassInitialize(TestContext context)
        {
            TestDAL DAL = new TestDAL();
            pizza = new PizzasController(DAL);

        }
       
        [TestMethod]
        public async Task GetListOfPizza()
        {
            //call method
           var actual =  await pizza.GetPizza();
            //the expected result value
            var expected = data.GetListOfPizza();
            //test the length of list
            Assert.AreEqual(2, actual.Count());
            //test if contain same value of pizza
            //not working, need to overwrite equal and hascode method in obejct
            //CollectionAssert.AreEquivalent(expected, actual.ToList());
            //CollectionAssert.AreEquivalent(expected, actual.ToList());
            //Assert.IsTrue(expected.SequenceEqual(actual));
        }

        //successfully get the pizza
        [TestMethod]
        public async Task GetPizza()
        {
            //call method
            var actual = await pizza.GetPizza(1,1);
            //the expected result value
            var expected = data.GetPizza();

            //OkObjectResult for return ok with object
            var okResult = actual as OkObjectResult;
            var value = okResult.Value as Pizza;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsInstanceOfType(okResult.Value,typeof(Pizza));
            Assert.AreEqual(expected.OrderID, value.OrderID);
            
        }

        //successfully create new pizza
        [TestMethod]
        public async Task PostPizza()
        {
            //get the pizza;
            var dummy = data.GetPizza();

            //call method
            var actual = await pizza.PostPizza(1,dummy);
            //okResult fore return without object
           var result = actual as OkResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public async Task PutPizza()
        {
            //get the pizza;
            var dummy = data.GetPizza();
            //assign list of toppig for calculating
            dummy.Meats = data.GetMeatsList();
            dummy.Veggies = data.GetVeggiesList();
            //call method
            var actual = await pizza.PutPizza(2, dummy);
            //okResult fore return without object
            var result = actual as OkResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        //successfully change quantity in shopping cart page
        [TestMethod]
        public async Task ChangeQuantity()
        {
            //get the pizza;
            var dummy = data.GetQuantityUpdate();
            //call method
            var actual = await pizza.ChangeQuantity(dummy);
            //okResult fore return without object
            var result = actual as OkObjectResult;
            var res = result.Value as PizzaAndOrder;
            var expected = data.GetPizzaAndOrder();

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOfType(result.Value, typeof(PizzaAndOrder));
            Assert.AreEqual(expected.PizzaPrice, res.PizzaPrice);
            Assert.AreEqual(expected.OrderTotalPrice, res.OrderTotalPrice);
            Assert.AreEqual(expected.OrderTotalQuantity, res.OrderTotalQuantity);

            
        }

        //successfully change quantity in shopping cart page
        [TestMethod]
        public async Task DeletePizza()
        {
            //get the pizza;
            var dummy = data.GetQuantityUpdate();
            //call method
            var actual = await pizza.DeletePizza(1);
            var result = actual as OkResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);


        }
    }
}
