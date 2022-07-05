using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.OrderAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class OrderRepository : RepositoryBase<long, Order>, IOrderRepository
    {
        private readonly ShopContext _context;

        public OrderRepository(ShopContext context):base(context)
        {
            _context = context;
        }
    }
}
