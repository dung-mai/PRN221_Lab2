using DataAccess.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DataAccess.Managers
{
    public class ProductManager
    {
        NorthwindContext _context;
        public ProductManager(NorthwindContext context)
        { _context = context; }

        public List<Product> GetProducts(int? categoryId)
        {
            if(categoryId == null || categoryId == 0)
            {
                return _context.Products.ToList();
            }
            return _context.Products.Where(p => p.CategoryId == categoryId).ToList();
        }

        public Product? GetProduct(int Id)
        {
            return _context.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.ProductId == Id);
        }

        public int DeleteProduct(int productId)
        {
            Product? product = _context.Products
                .FirstOrDefault(o => o.ProductId == productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                return 1;
            }
            return 0;
        }

        public int UpdateProduct(Product _product)
        {
            Product? product = _context.Products
                .FirstOrDefault(o => o.ProductId == _product.ProductId);
            if (product != null)
            {
                //Reflection.CopyProperties(_product, product);
                product.UnitsInStock = _product.UnitsInStock;
                return 1;
            }
            return 0;
        }
    }
}
