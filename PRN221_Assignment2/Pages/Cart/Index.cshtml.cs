using Bussiness.DTO;
using Bussiness.IRepository;
using Bussiness;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Bussiness.Repository;

namespace PRN221_Assignment2.Pages.Cart
{
    public class IndexModel : PageModel
    {
        public List<CategoryDTO>? Categories { get; set; }
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;

        public List<CartItem> Cart { get; set; }


        public IndexModel(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public void OnGet()
        {
            Cart = GetCartFromCookie();
            UpdateProductsInfo();
            Categories = _categoryRepository.GetCategories();
        }

        public IActionResult OnPostRemove(int productId)
        {
            List<CartItem> cart = GetCartFromCookie();
            CartItem item = cart.FirstOrDefault(p => p.Product.ProductId == productId);
            if (item != null)
            {
                cart.Remove(item);
                TempData["success"] = "Xóa khỏi giỏ hàng thành công!";
                AddToCookie(cart);
            }
            return RedirectToPage("/Cart/Index");
        }

        public IActionResult OnPostUpdate(int productId, int amount)
        {
            Categories = _categoryRepository.GetCategories();
            List<CartItem> cart = GetCartFromCookie();
            CartItem item = cart.FirstOrDefault(p => p.Product.ProductId == productId);
            if (item != null)
            {
                if (item.Quantity + amount == 0)
                {
                    cart.Remove(item);
                }
                else if (item.Quantity + amount <= item.Product.UnitsInStock)
                {
                    item.Quantity += amount;
                }
            }
            AddToCookie(cart);
            return RedirectToPage("/Cart/Index");
        }

        private void AddToCookie(List<CartItem> cart)
        {
            // config to skip property that cause infinite loop
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            // Convert from object to Json
            string updatedCartJson = JsonConvert.SerializeObject(cart, settings);
            CookieOptions options = new CookieOptions
            {
                // only access by http or https, not Javascript
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(7)
            };
            Response.Cookies.Append("cart", updatedCartJson, options);
        }

        private List<CartItem> GetCartFromCookie()
        {
            string cartJson = Request?.Cookies["cart"];
            if (cartJson != null)
            {
                // Convert from Json to object
                return JsonConvert.DeserializeObject<List<CartItem>>(cartJson);
            }
            else
            {
                return new List<CartItem>();
            }
        }

        private void UpdateProductsInfo()
        {
            Cart.ForEach(c =>
            {
                ProductDTO? product = _productRepository.GetProduct(c.Product.ProductId);
                //UPDATE PRODUCT 
                c.Product = product;
            });
            AddToCookie(Cart);
        }
    }
}
