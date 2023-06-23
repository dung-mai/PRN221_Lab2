using DataAccess.DataAccess.Models;

namespace DataAccess.DataAccess.Managers
{
    public class CategoryManager
    {
        NorthwindContext _context;
        public CategoryManager(NorthwindContext context)
        { _context = context; }

        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category? GetCategory(int Id)
        {
            return _context.Categories.FirstOrDefault(c => c.CategoryId == Id);
        }
    }
}
