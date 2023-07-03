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
    public class OrderDetailRepository : IOrderDetailRepository
    {
        NorthwindContext _context;
        IMapper _mapper;

        public OrderDetailRepository(NorthwindContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool Add(OrderDetailDTO orderDetailDTO)
        {
            OrderDetailManager orderDetailManager = new(_context);
            bool result = orderDetailManager.Add(_mapper.Map<OrderDetail>(orderDetailDTO));
            if (!result) return result;

            if (_context.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
