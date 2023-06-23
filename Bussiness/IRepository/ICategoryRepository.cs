using Bussiness.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.IRepository
{
    public interface ICategoryRepository
    {
        CategoryDTO GetCategory(int id);
        List<CategoryDTO> GetCategories();
    }
}
