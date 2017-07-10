using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Interfaces
{
    public interface IOrderRepository
    {
        Order LoadOrder(DateTime orderdate, int ordernumber);
        OrderSaveResponse SaveOrderToFile(Order order, DateTime orderdate);
        List<Order> GetAllOrders(DateTime orderdate);
        bool OrderFileExistsInDirectory(DateTime orderdate);
        OrderSaveResponse EditOrderInFile(Order ordertoedit, DateTime orderdate);
        void RemoveOrderInFile(Order ordertoremove, DateTime orderdate);
    }
}
