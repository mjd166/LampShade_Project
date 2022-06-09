using _01_LampshadeQuery.Contracts.Product;
using CommentManagement.Application.Contracts.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {

        private readonly ICommentApplication _commentApplication;

        

        public ProductQueryModel Product { get; set; }
        private readonly IProductQuery _productQuery;

        public ProductModel(IProductQuery productQuery, ICommentApplication commentApplication)
        {
            _productQuery = productQuery;
            _commentApplication = commentApplication;
        }

        public void OnGet(string id)
        {
            Product = _productQuery.GetDetails(id);
        }

        public IActionResult OnPost(AddComment command,string ProductSlug)
        {
            var result = _commentApplication.AddComment(command);
            return RedirectToPage("/Product", new { id = ProductSlug });
        }
    }
}
