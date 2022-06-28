using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceHost.Pages
{
    public class CartModel : PageModel
    {
        public List<CartItem> cartItems;
        public const string CookieName="cart_items";
        public void OnGet()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            cartItems = serializer.Deserialize <List< CartItem> > (value);
            foreach (var item in cartItems)
            {
                item.TotalItemPrice = item.UnitPrice * item.Count;
            }
        }


        public IActionResult OnGetRemoveFromCart(long id)
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            Response.Cookies.Delete(CookieName);
            var cartitems = serializer.Deserialize < List<CartItem>>(value);
            var itemtoremove = cartitems.FirstOrDefault(x => x.Id == id);

            cartitems.Remove(itemtoremove);

            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1)
            };

            
            Response.Cookies.Append(CookieName,serializer.Serialize(cartitems), options);

            return RedirectToPage("/Cart");
        }
    }
}
