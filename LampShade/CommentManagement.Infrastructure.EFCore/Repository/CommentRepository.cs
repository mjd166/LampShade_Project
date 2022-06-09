using _0_Framework.Application;
using _0_Framework.Infrastructure;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;
using System.Collections.Generic;
using System.Linq;

namespace CommentManagement.Infrastructure.EFCore.Repository
{
    public class CommentRepository : RepositoryBase<long, Comment>, ICommentRepository
    {
        private readonly CommentContext _context;
        public CommentRepository(CommentContext context) : base(context)
        {
            _context = context;
        }

        public List<CommentViewModel> Search(SearchViewModel searchViewModel)
        {
            var query = _context.Comments
                 .Select(x => new CommentViewModel
                 {
                     Id = x.Id,
                     Email = x.Email,
                     Website = x.Website,
                     IsCanceled = x.IsCanceled,
                     IsConfirm = x.IsConfirmed,
                     OwnerRecordId=x.OwnerRecordId,
                     Type=x.Type,

                     Message = x.Message,
                     Name = x.Name,
                     CreationDate = x.CreationDate.ToFarsi()


                 }) ;

            if (!string.IsNullOrWhiteSpace(searchViewModel.Name))
                query = query.Where(x => x.Name.Contains(searchViewModel.Name));

            if (!string.IsNullOrWhiteSpace(searchViewModel.Email))
                query = query.Where(x => x.Email.Contains(searchViewModel.Email));


            return query.OrderByDescending(x => x.Id).ToList();



        }
    }
}
