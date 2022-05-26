using _0_Framework.Application;
using ShopManagement.Application.Contracts.Comment;
using ShopManagement.Domain.CommentAgg;
using System.Collections.Generic;

namespace ShopManagement.Application
{
    public class CommentApplication : ICommentApplication
    {
        private readonly ICommentRepository _commentRepository;

        public CommentApplication(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public OperationResult AddComment(AddComment command)
        {
            var operationResult = new OperationResult();
            var comment = new Comment(command.Name, command.Email, command.Message, command.ProductId);
            _commentRepository.Create(comment);
            _commentRepository.Savechanges();
            return operationResult.Succedded();
        }

        public OperationResult Cancel(long id)
        {
            var operation = new OperationResult();
            var comment = _commentRepository.Get(id);
            if (comment == null) return operation.Failed(ApplicationMessages.RecordNotFound);
            comment.Cancel();
            _commentRepository.Savechanges();
            return operation.Succedded();
        }

        public OperationResult Confirm(long Id)
        {
            var operation = new OperationResult();
            var comment = _commentRepository.Get(Id);
            if (comment == null) return operation.Failed(ApplicationMessages.RecordNotFound);
            comment.Confirm();
            _commentRepository.Savechanges();
            return operation.Succedded();
        }
    

        public List<CommentViewModel> Search(SearchViewModel searchViewModel)
        {
            return _commentRepository.Search(searchViewModel);
        }
    }
}
