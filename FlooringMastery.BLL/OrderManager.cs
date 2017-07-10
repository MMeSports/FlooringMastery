using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using FlooringMastery.Models.Responses;
using FlooringMastery.Models.Interfaces;

namespace FlooringMastery.BLL
{
    public class OrderManager : IOrderRepository
    {
        private IOrderRepository _orderRepository;

        public OrderManager(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public OrderLookupResponse LookupOrder(DateTime orderdate, int orderNumber)
        {
            OrderLookupResponse response = new OrderLookupResponse();

            response.order = _orderRepository.LoadOrder(orderdate, orderNumber);

            if (response.order == null)
            {
                response.Success = false;
                response.Message = $"{orderNumber} is not a valid order.";
            }
            else
            {
                response.Success = true;
            }

            return response;
        }


        public List<Order> GetAllOrders(DateTime orderdate)
        {
            return _orderRepository.GetAllOrders(orderdate);
        }

        public bool OrderFileExistsInDirectory(DateTime date)
        {
            return _orderRepository.OrderFileExistsInDirectory(date);
        }

        public void RemoveOrderInFile(Order ordertoremove, DateTime orderdate)
        {
            _orderRepository.RemoveOrderInFile(ordertoremove, orderdate);
        }

        public OrderSaveResponse SaveOrderToFile(Order order, DateTime orderdate)
        {
            return _orderRepository.SaveOrderToFile(order, orderdate);
        }

        public Order LoadOrder(DateTime orderdate, int ordernumber)
        {
            return _orderRepository.LoadOrder(orderdate, ordernumber);
        }

        public OrderSaveResponse EditOrderInFile(Order ordertoedit, DateTime orderdate)
        {
            return _orderRepository.EditOrderInFile(ordertoedit, orderdate);
        }
    }
}
