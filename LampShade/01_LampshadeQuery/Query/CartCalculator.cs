using _0_Framework.Application;
using _0_Framework.Infrastructure;
using _01_LampshadeQuery.Contracts;
using DiscountManagement.Infrastructure.EFCore;
using ShopManagement.Application.Contracts.Order;
using System;
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
            var cart = new Cart();

            var colleagueDiscount = _discountContext.ColleagueDiscounts
                .Where(x=>!x.IsRemoved)
                .Select(x=>new
                {
                    x.DiscountRate,
                    x.ProductId
                }).ToList();
            var customerDiscount = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new
                {
                    x.DiscountRate,
                    x.ProductId

                }).ToList();
            var currentAccountRole = authHelper.CurrentAccountRole();
           
            foreach(var item in cartItems)
            {
                
                if(currentAccountRole == Roles.ColleagueUser)
                {
                    var colleaguedisc = colleagueDiscount.FirstOrDefault(x => x.ProductId == item.Id);
                    if (colleaguedisc == null) continue;
                    item.DiscountRate = colleaguedisc.DiscountRate;
                }
                else
                {
                    var customerdisc = customerDiscount.FirstOrDefault(x => x.ProductId == item.Id);
                    if (customerdisc == null) continue;
                    item.DiscountRate = customerdisc.DiscountRate;
                }
               
                item.DiscountAmount = ((item.TotalItemPrice * item.DiscountRate) / 100);
                item.ItemPayAmount = item.TotalItemPrice - item.DiscountAmount;

                cart.Add(item);


            }


            return cart;
        }
    }

   
}
