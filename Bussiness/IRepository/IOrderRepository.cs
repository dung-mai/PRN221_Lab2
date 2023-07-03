using Bussiness.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.IRepository
{
    public interface IOrderRepository
    {
        List<OrderDTO> GetOrders();
        List<OrderDTO> GetOrderById(int orderId);
        bool AddOrder(OrderDTO order);
        int GetLastInsertOrderId();
    }
}
