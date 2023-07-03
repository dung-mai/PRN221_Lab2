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
    public class ProductRepository : IProductRepository
    {
        NorthwindContext _context;
        IMapper _mapper;

        public ProductRepository(NorthwindContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<ProductDTO> GetProducts(int? categoryId)
        {
            ProductManager manager = new ProductManager(_context);
            List<Product> products = manager.GetProducts(categoryId);
            return products.Select(p => _mapper.Map<ProductDTO>(p)).ToList();
        }

        public List<ProductDTO> GetProducts(int? categoryId, int orderBy)
        {
            ProductManager manager = new ProductManager(_context);
            List<Product> products = manager.GetProducts(categoryId);
            if (orderBy == 2)
            {
                products = products.OrderByDescending(p => p.UnitPrice).ToList();
            }
            else
            {
                products = products.OrderBy(p => p.UnitPrice).ToList();
            }
            return products.Select(p => _mapper.Map<ProductDTO>(p)).ToList();
        }

        public ProductDTO? GetProduct(int id)
        {
            ProductManager manager = new ProductManager(_context);
            return _mapper.Map<ProductDTO>(manager.GetProduct(id));
        }

        public void UpdateUnitInStock(ProductDTO product, int quantity)
        {
            ProductManager manager = new ProductManager(_context);
            product.UnitsInStock -= (short)quantity;
            manager.UpdateProduct(_mapper.Map<Product>(product));
            _context.SaveChanges();
        }
    }
}
