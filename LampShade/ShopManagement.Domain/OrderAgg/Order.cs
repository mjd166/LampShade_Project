﻿using _0_Framework.Domain;
using System.Collections.Generic;

namespace ShopManagement.Domain.OrderAgg
{
    public class Order:EntityBase
    {
        public long AccountId { get; private set; }
        public int PaymentMethod { get; private set; }
        public double TotalAmount { get; private set; }   
        public double DiscountAmount { get; private set; }
        public double PayAmount { get; private set; }
        public bool IsPaid { get; private set; }
        public bool IsCanceled { get; private set; }
        public string IssueTrackingNumber { get; private set; }
        public long RefId { get; private set; }
        public List<OrderItem> Items { get; private set; }

        public Order(long accountId,int paymentMethod, double totalAmount, double discountAmount, double payAmount)
        {
            AccountId = accountId;
            PaymentMethod = paymentMethod;
            TotalAmount = totalAmount;
            DiscountAmount = discountAmount;
            PayAmount = payAmount;
            IsPaid = false;
            IsCanceled = false;
            RefId = 0;
            Items = new List<OrderItem>();
        }

        public void PaymentSucceeded(long refId)
        {
            IsPaid = true;
            if (refId != 0)
                this.RefId = RefId;
        }
        public void SetIssueTrackingNumber(string issueTrackingNo)
        {
            IssueTrackingNumber = issueTrackingNo;
        }
        public void Cancel()
        {
            IsCanceled = true;
        }

        public void Add(OrderItem item)
        {
            this.Items.Add(item);
        }
    }
}
