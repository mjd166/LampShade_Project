using System.Collections.Generic;

namespace ShopManagement.Application.Contracts.Order
{
    public class Cart
    {
        public double TotalAmount { get; set; }
        public double DiscountAmount { get; set; }
        public double PayAmount { get; set; }
        public List<CartItem> Items { get; set; }


        public Cart()
        {
            Items = new List<CartItem>();
        }

        public void Add(CartItem item)
        {
            Items.Add(item);
            TotalAmount += item.TotalItemPrice;
            DiscountAmount += item.DiscountAmount;
            PayAmount += item.ItemPayAmount;
        }


       
    }
}
