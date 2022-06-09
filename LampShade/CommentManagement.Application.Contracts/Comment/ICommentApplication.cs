using _0_Framework.Application;
using System.Collections.Generic;

namespace CommentManagement.Application.Contracts.Comment
{
    public interface ICommentApplication
    {
        OperationResult AddComment(AddComment command);
        OperationResult Confirm(long Id);
        OperationResult Cancel(long id);

        List<CommentViewModel> Search(SearchViewModel searchViewModel);
    }


   
}
