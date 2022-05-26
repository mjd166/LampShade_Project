using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Comment;
using ShopManagement.Domain.CommentAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class CommentRepository : RepositoryBase<long, Comment>, ICommentRepository
    {
        private readonly ShopContext _context;
        public CommentRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public List<CommentViewModel> Search(SearchViewModel searchViewModel)
        {
            var query = _context.Comments.Include(x => x.Product)
                 .Select(x => new CommentViewModel
                 {
                     Id = x.Id,
                     Email = x.Email,
                     IsCanceled = x.IsCanceled,
                     IsConfirm = x.IsConfirmed,
                     Message = x.Message,
                     Name = x.Name,
                     ProductId = x.ProductId,
                     ProductName = x.Product.Name,
                      CreationDate=x.CreationDate.ToFarsi()


                 });

            if (!string.IsNullOrWhiteSpace(searchViewModel.Name))
                query = query.Where(x => x.Name.Contains(searchViewModel.Name));

            if (!string.IsNullOrWhiteSpace(searchViewModel.Email))
                query = query.Where(x => x.Email.Contains( searchViewModel.Email));


            return query.OrderByDescending(x => x.Id).ToList();



        }
    }
}
