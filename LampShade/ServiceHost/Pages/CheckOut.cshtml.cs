using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Pages
{
    public class CheckOutModel : PageModel
    {
        public Cart Cart;
        const string cookieName = "cart_items";
        public void OnGet()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[cookieName];

            var cartitems = serializer.Deserialize<List<CartItem>>(value);

            foreach (var item in cartitems)
                item.CalculateTotalItemPrice();
            
        }
    }
}
