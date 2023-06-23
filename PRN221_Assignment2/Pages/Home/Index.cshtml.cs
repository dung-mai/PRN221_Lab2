using Bussiness.DTO;
using Bussiness.IRepository;
using Bussiness.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PRN221_Assignment2.Pages.Home
{
    public class IndexModel : PageModel
    {
        public List<CategoryDTO> Categories { get; set; }
        private ICategoryRepository _categoryRepository;

        public IndexModel(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public void OnGet()
        {
            Categories = _categoryRepository.GetCategories();
        }
    }
}
