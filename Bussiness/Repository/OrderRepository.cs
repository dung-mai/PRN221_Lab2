using AutoMapper;
using Bussiness.DTO;
using Bussiness.IRepository;
using DataAccess.DataAccess.Managers;
using DataAccess.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Repository
{
    public class OrderRepository : IOrderRepository
    {
        NorthwindContext _context;
        IMapper _mapper;

        public OrderRepository(NorthwindContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool AddOrder(OrderDTO order)
        {
            OrderManager orderManager = new(_context);
            bool result = orderManager.AddOrder(_mapper.Map<Order>(order));
            if (!result) return result;

            if(_context.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        public int GetLastInsertOrderId()
        {
            OrderManager orderManager = new(_context);
            return orderManager.GetLastInsertOrderId();
        }

        public List<OrderDTO> GetOrderById(int orderId)
        {
            throw new NotImplementedException();
        }

        public List<OrderDTO> GetOrders()
        {
            throw new NotImplementedException();
        }
    }
}
