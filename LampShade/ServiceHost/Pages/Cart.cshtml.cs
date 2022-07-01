using _01_LampshadeQuery.Contracts.Product;
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
        public List<CartItem> CartItems;
        public const string CookieName = "cart_items";
        private readonly IProductQuery _productQuery;

        public CartModel(IProductQuery productQuery)
        {
            CartItems = new List<CartItem>();
            _productQuery = productQuery;
        }

        public void OnGet()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(value);

            foreach (var item in cartItems)
                item.CalculateTotalItemPrice();

            CartItems = _productQuery.CheckInventoryStatus(cartItems);

        }


        public IActionResult OnGetRemoveFromCart(long id)
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            Response.Cookies.Delete(CookieName);
            var cartitems = serializer.Deserialize<List<CartItem>>(value);
            var itemtoremove = cartitems.FirstOrDefault(x => x.Id == id);

            cartitems.Remove(itemtoremove);

            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1)
            };


            Response.Cookies.Append(CookieName, serializer.Serialize(cartitems), options);

            return RedirectToPage("/Cart");
        }

        public IActionResult OnGetGoToCheckOut()
        {
            var serializer = new JavaScriptSerializer();

            var value = Request.Cookies[CookieName];
            var cartitems = serializer.Deserialize<List<CartItem>>(value);
            foreach (var item in cartitems)
            {
                item.TotalItemPrice = item.UnitPrice * item.Count;

            }

            CartItems = _productQuery.CheckInventoryStatus(cartitems);
            return CartItems.Any(x => !x.IsInStock) ? RedirectToPage("/Cart") : RedirectToPage("/CheckOut");
        }
    }
}
