using FlooringMastery.BLL;
using FlooringMastery.Data;
using FlooringMastery.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Tests
{
    [TestFixture]
    class OrderManagerTests
    {
        OrderManager ordermanager;
        Order TestOrder;
        DateTime TestDate;

        [SetUp]
        public void beforetests()
        {
            TestDate = DateTime.Now;
            TestOrder = new Order()
            {
                OrderNumber = 0,
                CustomerName = "I AM A TEST",
                State = "IN",
                TaxRate = 6.00M,
                ProductType = "Wood",
                Area = 100,
                CostPerSquareFoot = 5.15M,
                LaborCostPerSquareFoot = 4.75M
            };
        }

        [Test]
        public void ManagerAddsOrderToList()
        {
            ordermanager = OrderManagerFactory.Create();

            ordermanager.SaveOrderToFile(TestOrder, TestDate);
            var allorders = ordermanager.GetAllOrders(TestDate);

            Assert.AreEqual(4, allorders.Count());
        }

        [Test]
        public void ManagerRemovesOrderFromList()
        {
            ordermanager = OrderManagerFactory.Create();

            ordermanager.SaveOrderToFile(TestOrder, TestDate);
            ordermanager.RemoveOrderInFile(TestOrder, TestDate);

            var allorders = ordermanager.GetAllOrders(TestDate);

            Assert.AreEqual(3, allorders.Count);
        }

        [Test]
        public void ManagerEditsOrderList()
        {
            ordermanager = OrderManagerFactory.Create();

            ordermanager.SaveOrderToFile(TestOrder, TestDate);

            var ordertoedit = new Order()
            {
                OrderNumber = 0,
                CustomerName = "EDITED",
                Area = 500.00m,
                State = "MI"
            };
            var allorders = ordermanager.GetAllOrders(TestDate);

            ordermanager.EditOrderInFile(ordertoedit, TestDate);
            var EditedOrder = allorders.FirstOrDefault(o => o.OrderNumber == ordertoedit.OrderNumber);

            Assert.AreEqual("EDITED", EditedOrder.CustomerName);
        }
    }
}
