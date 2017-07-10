using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models;
using FlooringMastery.Models.Responses;
using System.IO;

namespace FlooringMastery.Data
{
    public class FileRepository : IOrderRepository
    {
        string headerLine = "OrderNumber|CustomerName|State|TaxRate|ProductType|Area|CostPerSquareFoot|LaborCostPerSquareFoot|MaterialCost|LaborCost|Tax|Total";

        public Order LoadOrder(DateTime orderdate, int ordernumber)
        {
            List<Order> allOrdersFromFile = GetAllOrders(orderdate);

            var orderToLookup = allOrdersFromFile.FirstOrDefault(o => o.OrderNumber == ordernumber);

            return orderToLookup;
        }
        public List<Order> GetAllOrders(DateTime orderdate)
        {
            var newPath = CreateFilePathFromDate(orderdate);
            List<Order> allOrderFromFile = new List<Order>();
            var allLinesFromFile = File.ReadAllLines(newPath).Skip(1).ToList();

            foreach(var line in allLinesFromFile)
            {
                string[] column = line.Split(',');
                
                Order o = new Order();
                o.OrderNumber = int.Parse(column[0]);
                o.CustomerName = column[1];
                o.State = column[2];
                o.TaxRate = decimal.Parse(column[3]);
                o.ProductType = column[4];
                o.Area = decimal.Parse(column[5]);
                o.CostPerSquareFoot = decimal.Parse(column[6]);
                o.LaborCostPerSquareFoot = decimal.Parse(column[7]);
                allOrderFromFile.Add(o);
            }
            return allOrderFromFile;
        }

        public string CreateFilePathFromDate(DateTime orderdate)
        {
            string formattedOrderDate = $"Orders_{orderdate.ToString("MMddyyyy")}.txt";
            string newPath = @"C:\Users\praisekek\Desktop\OrderData\" + formattedOrderDate;
            return newPath;
        }

        public bool OrderFileExistsInDirectory(DateTime date)
        {
            var path = CreateFilePathFromDate(date);
            if (File.Exists(path))
                return true;
            else return false;
        }

        public OrderSaveResponse SaveOrderToFile(Order order, DateTime orderdate)
        {
            var response = OrderFileExistsInDirectory(orderdate);
            var ordersaveresponse = new OrderSaveResponse();

            if (response == true)
            {
                var path = CreateFilePathFromDate(orderdate);
                var allLinesFromFile = File.ReadAllLines(path);
                var orderLine = $"{order.OrderNumber},{order.CustomerName},{order.State},{order.TaxRate},{order.ProductType},{order.Area},{order.CostPerSquareFoot},{order.LaborCostPerSquareFoot},{order.MaterialCost},{order.LaborCost},{order.Tax},{order.Total}";
                List<string> allLinesToSave = new List<string>();
                allLinesToSave.Add(orderLine);

                File.AppendAllLines(path, allLinesToSave);

                ordersaveresponse.Success = true;
                ordersaveresponse.Message = $"New order added to {orderdate}.";
                return ordersaveresponse;

            }  else if (response == false)
            {
                var path = CreateFilePathFromDate(orderdate);
                var orderLine = $"{order.OrderNumber},{order.CustomerName},{order.State},{order.TaxRate},{order.ProductType},{order.Area},{order.CostPerSquareFoot},{order.LaborCostPerSquareFoot},{order.MaterialCost},{order.LaborCost},{order.Tax},{order.Total}";
                List<string> allLinesToSave = new List<string>();
                allLinesToSave.Add(headerLine);
                allLinesToSave.Add(orderLine);

                File.AppendAllLines(path, allLinesToSave);

                ordersaveresponse.Success = true;
                ordersaveresponse.Message = "New order and file created.";
                return ordersaveresponse;
            }
            else
            {
                ordersaveresponse.Success = false;
                ordersaveresponse.Message = "An error occured, unable to save order to file.  Contact IT!";
                return ordersaveresponse;
            }
        }

        public void RemoveOrderInFile(Order ordertoremove, DateTime orderdate)
        {
            var header = "OrderNumber|CustomerName|State|TaxRate|ProductType|Area|CostPerSquareFoot|LaborCostPerSquareFoot|MaterialCost|LaborCost|Tax|Total";
            var allCurrentOrders = GetAllOrders(orderdate);
            //take all the old orders except for the order number to be removed
            var newOrderList = allCurrentOrders.Where(o => o.OrderNumber != ordertoremove.OrderNumber).ToList();

            List<string> newFile = new List<string>();
            newFile.Add(header);
            foreach (var order in newOrderList)
            {
                var orderLine = $"{order.OrderNumber},{order.CustomerName},{order.State},{order.TaxRate},{order.ProductType},{order.Area},{order.CostPerSquareFoot},{order.LaborCostPerSquareFoot},{order.MaterialCost},{order.LaborCost},{order.Tax},{order.Total}.";
                newFile.Add(orderLine);
            }
            var path = CreateFilePathFromDate(orderdate);
            File.Delete(path);
            File.WriteAllLines(path, newFile);
        }

        public OrderSaveResponse EditOrderInFile(Order ordertoedit, DateTime orderdate)
        {
            var header = "OrderNumber|CustomerName|State|TaxRate|ProductType|Area|CostPerSquareFoot|LaborCostPerSquareFoot|MaterialCost|LaborCost|Tax|Total";

            var allCurrentOrders = GetAllOrders(orderdate);

            var newOrderList = allCurrentOrders.Where(o => o.OrderNumber != ordertoedit.OrderNumber).ToList();

            newOrderList.Add(ordertoedit);

            newOrderList = newOrderList.OrderBy(o => o.OrderNumber).ToList();

            var path = CreateFilePathFromDate(orderdate);
            List<string> newFileData = new List<string>();
            newFileData.Add(header);

            foreach (var order in newOrderList.OrderBy(o => o.OrderNumber))
            {
                var orderLine = $"{order.OrderNumber},{order.CustomerName},{order.State},{order.TaxRate},{order.ProductType},{order.Area},{order.CostPerSquareFoot},{order.LaborCostPerSquareFoot},{order.MaterialCost},{order.LaborCost},{order.Tax},{order.Total}.";
                newFileData.Add(orderLine);
            }

            File.Delete(path);
            File.WriteAllLines(path, newFileData);

            var response = new OrderSaveResponse();
            response.Success = true;
            response.Message = "Order edited in file.";
            return response;
        }
    }
}
