using Bussiness.DTO;
using Bussiness.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PRN221_Assignment2.Pages.Cart
{
    public class SuccessModel : PageModel
    {
        private ICategoryRepository _categoryRepository;
        public List<CategoryDTO>? Categories { get; set; }

        public SuccessModel(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public void OnGet(int? code)
        {
            Categories = _categoryRepository.GetCategories();
            ViewData["OrderCode"] = code;
        }
    }
}
