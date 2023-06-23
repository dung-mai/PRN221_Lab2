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
    public class CategoryRepository : ICategoryRepository
    {
        NorthwindContext _context;
        IMapper _mapper;

        public CategoryRepository(NorthwindContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<CategoryDTO> GetCategories()
        {
            CategoryManager manager = new CategoryManager(_context);
            return manager.GetCategories().Select(c => _mapper.Map<CategoryDTO>(c)).ToList();
        }

        public CategoryDTO GetCategory(int id)
        {
            throw new NotImplementedException();
        }
    }
}
