using _0_Framework.Application;
using _0_Framework.Infrastructure;
using _01_LampshadeQuery.Contracts;
using DiscountManagement.Infrastructure.EFCore;
using ShopManagement.Application.Contracts.Order;
using System.Collections.Generic;
using System.Linq;

namespace _01_LampshadeQuery.Query
{
    public class CartCalculator : ICartCalculatorService
    {
        private readonly IAuthHelper authHelper;
        private readonly DiscountContext _discountContext;

        public CartCalculator(DiscountContext discountContext, IAuthHelper authHelper)
        {
            _discountContext = discountContext;
            this.authHelper = authHelper;
        }

        public Cart ComputeCart(List<CartItem> cartItems)
        {
            var result = new Cart();

            var colleagueDiscount = _discountContext.ColleagueDiscounts
                .Where(x=>!x.IsRemoved)
                .Select(x=>new
                {
                    x.DiscountRate,
                    x.ProductId
                }).ToList();
            var currentAccountRole = authHelper.CurrentAccountRole();

            if (currentAccountRole == Roles.ColleagueUser)
            {

            }
            else
            {

            }

            return result;
        }
    }
}
