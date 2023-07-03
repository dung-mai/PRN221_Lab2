using Microsoft.AspNetCore.Mvc;
using Bussiness.DTO;
using Newtonsoft.Json;

namespace PRN221_Assignment2.Pages.Shared.Components.CartList
{
    public class CartListViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cart = GetCartFromCookie();
            return View(cart);
        }

        private List<CartItem> GetCartFromCookie()
        {
            string cartJson = Request.Cookies["cart"];
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
