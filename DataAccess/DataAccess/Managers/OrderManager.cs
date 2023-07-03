using DataAccess.DataAccess.Models;

namespace DataAccess.DataAccess.Managers
{
    public class OrderManager
    {
        NorthwindContext _context;
        public OrderManager(NorthwindContext context)
        { _context = context; }

        public List<Order> GetOrders(int StartIndex, int Size)
        {
            return _context.Orders
                .Skip(StartIndex - 1)
                .Take(Size)
                .ToList();
        }

        public int DeleteOrder(int OrderId)
        {
            Order? order = _context.Orders
                .FirstOrDefault(o => o.OrderId == OrderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
                return 1;
            }
            return 0;
        }

        public bool AddOrder(Order order)
        {
            try
            {
                _context.Orders.Add(order);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int GetLastInsertOrderId()
        {
            Order? order = _context.Orders.OrderBy(o => o.OrderId).LastOrDefault();
            return  order != null ? order.OrderId : 0;
        }
    }
}
