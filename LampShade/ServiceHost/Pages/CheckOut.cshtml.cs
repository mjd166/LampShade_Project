using System.Collections.Generic;
using System.Linq;
using _01_LampshadeQuery.Contracts;
using _01_LampshadeQuery.Contracts.Product;
using _01_LampshadeQuery.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Pages
{
    public class CheckOutModel : PageModel
    {
        public Cart Cart;
        const string cookieName = "cart_items";
        private readonly ICartCalculatorService _cartCalculator;
        private readonly ICartService _cartService;
        private readonly IProductQuery _productQuery;
        public CheckOutModel(ICartCalculatorService cartCalculator, ICartService cartService, IProductQuery productQuery)
        {
            _cartCalculator = cartCalculator;
            _cartService = cartService;
            _productQuery = productQuery;
        }

        public void OnGet()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[cookieName];

            var cartitems = serializer.Deserialize<List<CartItem>>(value);

            foreach (var item in cartitems)
                item.CalculateTotalItemPrice();

            Cart = _cartCalculator.ComputeCart(cartitems);

            _cartService.Set(Cart);
            
            
        }

        public IActionResult OnGetPay()
        {
            var cart = _cartService.Get();
           var result= _productQuery.CheckInventoryStatus(cart.Items);
            if (result.Any(x => !x.IsInStock))
                return RedirectToPage("/Cart");

            return RedirectToPage("/CheckOut");
        }
    }
}
