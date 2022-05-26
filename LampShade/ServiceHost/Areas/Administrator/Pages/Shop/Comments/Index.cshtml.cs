using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Comment;

namespace ServiceHost.Areas.Administrator.Pages.Shop.Comments
{
    public class IndexModel : PageModel
    {

        [TempData]
        public string Message { get; set; }
        public SearchViewModel SearchModel;
        private readonly ICommentApplication _commentApplication;
        public List<CommentViewModel> Comments;
        public IndexModel(ICommentApplication commentApplication)
        {
            _commentApplication = commentApplication;
        }

        public void OnGet(SearchViewModel SearchModel)
        {

            Comments = _commentApplication.Search(SearchModel);

        }

       public IActionResult OnGetConfirm(long id)
        {
            var result = _commentApplication.Confirm(id);
            if (result.IsSuccedded)
                return RedirectToPage("./Index");

            else
                return RedirectToPage("./Index");
        }

        public IActionResult OnGetCancel(long id)
        {
            var result = _commentApplication.Cancel(id);
            if (result.IsSuccedded)
                return RedirectToPage("./Index");

            else
                return RedirectToPage("./Index");
        }

       
    }
}
