using DataAccess.DataAccess.Models;

namespace DataAccess.DataAccess.Managers
{
    public class OrderDetailManager
    {
        NorthwindContext _context;
        public OrderDetailManager(NorthwindContext context)
        { _context = context; }

        public bool Add(OrderDetail orderDetail)
        {
            try
            {
                _context.OrderDetails.Add(orderDetail);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
