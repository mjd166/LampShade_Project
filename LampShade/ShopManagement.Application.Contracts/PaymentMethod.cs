using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Application.Contracts
{
    public class PaymentMethod
    {
        public int Id { get;private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        private PaymentMethod(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public static List<PaymentMethod> GetList()
        {
            return new List<PaymentMethod>
            {
                new PaymentMethod(1,"پرداخت آنلاین","پرداخت اینترنتی از طریق درگاه های اینترنتی"),
                new PaymentMethod(2,"پرداخت نقدر","پرداخت در هنگام تحویل کالا و دم در ")
            };


        }
        public static PaymentMethod GetBy(int id)
        {
            return GetList().FirstOrDefault(x => x.Id == id);
        }
    }
}
