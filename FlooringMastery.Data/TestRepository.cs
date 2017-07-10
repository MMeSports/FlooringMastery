using FlooringMastery.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using FlooringMastery.Models.Responses;

namespace FlooringMastery.Data
{
    public class TestRepository : IOrderRepository
    {

        List<Order> _testrepo;

        public TestRepository()
        {
            if (_testrepo == null)
            {
                _testrepo =
                    new List<Order>
                    {

                        new Order
                        {
                            OrderNumber = 1,
                            CustomerName = "John Cena",
                            State = "OH",
                            TaxRate = 6.25M,
                            ProductType = "Wood",
                            Area = 100M,
                            CostPerSquareFoot = 5.15M,
                            LaborCostPerSquareFoot = 4.75M
                        },
                        new Order
                        {
                            OrderNumber = 2,
                            CustomerName = "Marty McFly",
                            State = "PA",
                            TaxRate = 6.75M,
                            ProductType = "Wood",
                            Area = 200M,
                            CostPerSquareFoot = 5.15M,
                            LaborCostPerSquareFoot = 4.75M
                        },
                        new Order
                        {
                            OrderNumber = 3,
                            CustomerName = "Shrek",
                            State = "IN",
                            TaxRate = 6.00M,
                            ProductType = "Wood",
                            Area = 100M,
                            CostPerSquareFoot = 5.15M,
                            LaborCostPerSquareFoot = 4.75M
                        }

                    };
            }
        }

        public string CreateFilePathFromDate(DateTime orderdate)
        {
            throw new NotImplementedException();
        }

        public OrderSaveResponse EditOrderInFile(Order ordertoedit, DateTime orderdate)
        {
            var allOrders = GetAllOrders();

            var selectedorder = LoadOrder(orderdate, ordertoedit.OrderNumber);
            var response = new OrderSaveResponse();

            if (selectedorder == null)
            {
                response.Success = false;
                return response;
            }
            else
            {
                selectedorder.CustomerName = ordertoedit.CustomerName;
                selectedorder.Area = ordertoedit.Area;
                selectedorder.State = ordertoedit.State;

                var newAllOrders = allOrders.TakeWhile(o => o.OrderNumber != ordertoedit.OrderNumber).ToList();

                newAllOrders.Add(ordertoedit);
                newAllOrders.OrderBy(o => o.OrderNumber);

                response.Success = true;
                return response;
            }
        }

        public List<Order> GetAllOrders(DateTime orderdate)
        {
            return _testrepo;
        }

        public Order LoadOrder(DateTime orderdate, int ordernumber)
        {
            var allOrders = GetAllOrders();

            var selectedorder = allOrders.FirstOrDefault(o => o.OrderNumber == ordernumber);

            return selectedorder;
        }

        public bool OrderFileExistsInDirectory(DateTime orderdate)
        {
            return true;
        }

        public void RemoveOrderInFile(Order ordertoremove, DateTime orderdate)
        {
            var allorders = GetAllOrders();
            var selectedorder = allorders.FirstOrDefault(o => o.OrderNumber == ordertoremove.OrderNumber);
            allorders.Remove(selectedorder);
        }

        public OrderSaveResponse SaveOrderToFile(Order order, DateTime orderdate)
        {
            _testrepo.Add(order);
            var response = new OrderSaveResponse();
            response.Success = true;
            return response;
        }

        public List<Order> GetAllOrders()
        {
            return _testrepo;
        }
    }
}
