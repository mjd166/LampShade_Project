using System.Collections.Generic;

namespace ShopManagement.Application.Contracts.Order
{
    public class Cart
    {
        public double TotalAmount { get; set; }
        public double DiscountAmount { get; set; }
        public double PayAmount { get; set; }
        public List<CartItem> CartItems { get; set; }


        public Cart()
        {
            CartItems = new List<CartItem>();
        }

    }
}
