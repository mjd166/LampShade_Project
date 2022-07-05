using ShopManagement.Application.Contracts.Order;
using System;

namespace ShopManagement.Application
{
    public class CartService : ICartService
    {
        public Cart cart;
        public Cart Get()
        {
            return this.cart;
        }

        public void Set(Cart cart)
        {
            this.cart = cart;
        }
    }
}
