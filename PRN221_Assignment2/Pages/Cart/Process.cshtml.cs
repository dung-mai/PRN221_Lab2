using Bussiness.DTO;
using Bussiness.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PRN221_Assignment2.Extensions;

namespace PRN221_Assignment2.Pages.Cart
{
    public class ProcessModel : PageModel
    {
        public List<CategoryDTO>? Categories { get; set; }
        private ICategoryRepository _categoryRepository;
        private IOrderRepository _orderRepository;
        private IProductRepository _productRepository;
        private IOrderDetailRepository _orderDetailRepository;

        public List<CartItem> Cart { get; set; }
        public List<CartItem> PurchaseCart { get; set; }


        public ProcessModel(ICategoryRepository categoryRepository, IOrderDetailRepository orderDetailRepository, IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _orderDetailRepository = orderDetailRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public void OnGet(int[] purchase_product)
        {
            Cart = GetCartFromCookie();
            PurchaseCart = GetPurchaseCart(purchase_product);
            HttpContext.Session.Set<List<CartItem>>("cart_product_list", PurchaseCart);
            Categories = _categoryRepository.GetCategories();
        }

        private List<CartItem> GetPurchaseCart(int[] purchase_product)
        {
            List<CartItem> cart = new List<CartItem>();
            for (int i = 0; i < purchase_product.Length; i++)
            {
                CartItem? item = Cart.FirstOrDefault(c => c.Product.ProductId == purchase_product[i]);
                if (item != null)
                {
                    cart.Add(item);
                }
            }
            return cart;
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

        public IActionResult OnPost(string? fullname, string? email, string? phoneNumber, string? address, string? city, string? district, string? ward, string? paymentMedthod)
        {
            Cart = GetCartFromCookie();
            PurchaseCart =  HttpContext.Session.Get<List<CartItem>>("cart_product_list");
            if (PurchaseCart.Count > 0)
            {
                //Check if product is availble
                if (!CheckAvailbleProduct())
                {
                    TempData["success"] = "Đặt hàng thất bại!";
                    return RedirectToPage("/Cart/Index");
                }

                OrderDTO order = new()
                {
                    ShipCountry = "VietNam",
                    ShipCity = city,
                    ShipRegion = district,
                    ShipAddress = address + ", " + ward,
                    ShipName = fullname,
                    OrderDate = DateTime.Now,
                    ShipVia = paymentMedthod.Equals("cod") ? 1 : 0,
                    Email = email,
                    PhoneNumber = phoneNumber
                };

                if (!_orderRepository.AddOrder(order))
                {
                    TempData["success"] = "Đặt hàng thất bại!";
                    return RedirectToPage("/Cart/Index");
                }

                int orderId = _orderRepository.GetLastInsertOrderId();
                AddOrderDetail(orderId);

                TempData["success"] = "Đặt hàng thành công!";
                UpdateCookie(PurchaseCart);
                return RedirectToPage("/Cart/Success", new { code = orderId });
            }
            TempData["success"] = "Đặt hàng thất bại!";
            return RedirectToPage("/Cart/Index");
        }

        private void AddOrderDetail(int orderId)
        {
            PurchaseCart.ForEach(c =>
            {
                _orderDetailRepository.Add(
                new OrderDetailDTO
                {
                    OrderId = orderId,
                    UnitPrice = (decimal)c.Product.PromotionPrice,
                    ProductId = c.Product.ProductId,
                    Quantity = (short)c.Quantity
                });

                _productRepository.UpdateUnitInStock(c.Product, c.Quantity);
            });
        }

        private void UpdateCookie(List<CartItem> buyCart)
        {
            for (int i = 0; i < Cart.Count; i++)
            {
                for (int j = 0; j < buyCart.Count; j++)
                {
                    if (Cart[i].Product.ProductId == buyCart[j].Product.ProductId)
                    {
                        Cart.Remove(Cart[i]);
                    }
                }
            }

            AddToCookie(Cart);
        }

        bool CheckAvailbleProduct()
        {
            foreach (var cartItem in PurchaseCart)
            {
                ProductDTO? product = _productRepository.GetProduct(cartItem.Product.ProductId);
                if (product == null || product.UnitsInStock < cartItem.Quantity)
                {
                    return false;
                }
            }
            return true;
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
    }
}
