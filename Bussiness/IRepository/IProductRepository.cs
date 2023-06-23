using Bussiness.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.IRepository
{
    public interface IProductRepository
    {
        List<ProductDTO> GetProducts(int? categoryId);
        List<ProductDTO> GetProducts(int? categoryId, int orderBy);
        ProductDTO? GetProduct(int id);
    }
}
