using Bussiness;
using Bussiness.DTO;
using Bussiness.IRepository;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace PRN221_Assignment2.Pages.Product
{
    public class ShopModel : PageModel
    {
        public List<CategoryDTO>? Categories { get; set; }
        public List<ProductDTO>? Products { get; set; }
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;

        public Pagination Page { get; set; }
        public int[] AvailPageSize { get; } = new int[] { 8, 12, 16 };

        public int SelectedCategory { get; set; }

        public ShopModel(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public void OnGet(int? cid)
        {
            Categories = _categoryRepository.GetCategories();
            Products = _productRepository.GetProducts(cid);
            Page = new Pagination(Products.Count);
            Products = Products.Skip(Page.StartIndex).Take(Page.Pagesize).ToList();
            ViewData["SelectedCategory"] = cid.HasValue ? (int) cid : 0;
        }

        public IActionResult OnPostFilterProduct(int? cid, int? orderBy,int? page, int? pagesize)
        {
            Categories = _categoryRepository.GetCategories();
            Products = _productRepository.GetProducts(cid, orderBy ?? 1);
            pagesize = pagesize.HasValue ? pagesize : AvailPageSize[0];
            Page = new Pagination(Products.Count, page, (int) pagesize);
            Products = Products.Skip(Page.StartIndex).Take(Page.Pagesize).ToList();
            ViewData["SelectedCategory"] = cid.HasValue ? (int)cid : 0;
            ViewData["SelectSort"] = orderBy != null ? orderBy : 1;
            return Page();
        }

        public IActionResult OnPostAddToCart(int? productId, int? quantity = 1)
        {
            if (productId == null)
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
                    ProductDTO product = new ProductDTO();
                    ProductDTO findProduct = _productRepository.GetProduct((int)productId);
                    if (findProduct != null)
                    {
                        Reflection.CopyProperties(findProduct, product);
                        product.Category.Picture = null;
                        product.Category.Products = null;
                    }
                    item = new()
                    {
                        Product = product,
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

    }
}
