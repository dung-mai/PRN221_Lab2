using Bussiness;
using Bussiness.DTO;
using DataAccess;
using Bussiness.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace PRN221_Assignment2.Pages.Product
{
    public class ProductDetailModel : PageModel
    {

        public List<CategoryDTO> Categories { get; set; }
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;
        public ProductDTO? Product { get; set; }
        private string cart_name = "cart";

        public ProductDetailModel(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public void OnGet(int? id)
        {
            Categories = _categoryRepository.GetCategories();
            if (id.HasValue)
            {
                Product = _productRepository.GetProduct((int)id);
            }
        }

        public IActionResult OnGetUpdateCartNumberComp()
        {
            return ViewComponent("CartNumber");
        }

        public IActionResult OnPostAddToCart(int? productId, int? quantity = 1)
        {
            if(productId == null )
            {
                return new JsonResult(new { success = false });
            }
            List<CartItem> cart = GetCartFromCookie();
            try
            {
                //Add product to cart
                CartItem? item = cart.FirstOrDefault(p => p.Product?.ProductId == productId);
                if (item != null)
                {
                    if (quantity != null)
                    {
                        item.Quantity += (int)quantity;
                    }
                    else
                    {
                        item.Quantity++;
                    }
                }
                else
                {
                    Product = new ProductDTO();
                    ProductDTO findProduct = _productRepository.GetProduct((int)productId);
                    if(findProduct != null)
                    {
                        Reflection.CopyProperties(findProduct, Product);
                        Product.Category = null;
                    }
                    item = new()
                    {
                        Product = Product,
                        Quantity = (quantity != null) ? quantity.Value : 1
                    };
                    cart.Add(item);
                }
                TempData["success"] = "Thêm vào giỏ hàng thành công!";
                AddToCookie(cart);
                return new JsonResult(new { success = true });
            }
            catch (Exception)
            {
                return new JsonResult(new { success = false });
            }
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
            Response.Cookies.Append(cart_name, updatedCartJson, options);
        }

        private List<CartItem> GetCartFromCookie()
        {
            string cartJson = Request?.Cookies[cart_name];
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
    }
}
