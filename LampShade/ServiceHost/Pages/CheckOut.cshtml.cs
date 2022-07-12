using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Application.ZarinPal;
using _01_LampshadeQuery.Contracts;
using _01_LampshadeQuery.Contracts.Product;
using _01_LampshadeQuery.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Pages
{
    [Authorize]
    public class CheckOutModel : PageModel
    {
        public Cart Cart;
        const string cookieName = "cart_items";
        private readonly ICartCalculatorService _cartCalculator;
        private readonly ICartService _cartService;
        private readonly IProductQuery _productQuery;
        private readonly IOrderApplication _orderApplication;
        private readonly IZarinPalFactory _zarinPalFactory;
        private readonly IAuthHelper _authHelper;
        public CheckOutModel(ICartCalculatorService cartCalculator, ICartService cartService, IProductQuery productQuery, IOrderApplication orderApplication, IZarinPalFactory zarinPalFactory, IAuthHelper authHelper)
        {
            _cartCalculator = cartCalculator;
            _cartService = cartService;
            _productQuery = productQuery;
            _orderApplication = orderApplication;
            _zarinPalFactory = zarinPalFactory;
            _authHelper = authHelper;
            Cart = new Cart();
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
            var result = _productQuery.CheckInventoryStatus(cart.Items);
            if (result.Any(x => !x.IsInStock))
                return RedirectToPage("/Cart");

            var orderId = _orderApplication.PlaceOrder(cart);
            var username = _authHelper.CurrentAccountInfo().Username;
            var PaymentResult = _zarinPalFactory.CreatePaymentRequest(cart.PayAmount.ToString(), "", "", "خرید از فروشگاه لمپ شید", orderId);


            return Redirect(
                    $"https://{_zarinPalFactory.Prefix}.zarinpal.com/pg/StartPay/{PaymentResult.Authority}");
            // return RedirectToPage("/CheckOut");
        }

        public IActionResult OnGetCallBack([FromQuery] string authority, [FromQuery] string status, [FromQuery] long oId)
        {
            var orderPayment = _orderApplication.GetAmountBy(oId);
            var verifyResult = _zarinPalFactory.CreateVerificationRequest(authority, orderPayment.ToString(CultureInfo.InvariantCulture));

            var result = new PaymentResult();
            if (status == "Ok" && verifyResult.Status >= 100)
            {
               string IssueTrackingNumber= _orderApplication.OrderSucceeded(oId, verifyResult.RefID);
                Response.Cookies.Delete("cart_items");

                return RedirectToPage("/PaymentResult", result.Succeeded("عملیات پرداخت با موفقیت انجام شد",IssueTrackingNumber));
            }
            else
            {
                result = result.Failed("عملیات پرداخت ناموفق بود");
                return RedirectToPage("/PaymentResult", result);
            }
        }



    }
}
