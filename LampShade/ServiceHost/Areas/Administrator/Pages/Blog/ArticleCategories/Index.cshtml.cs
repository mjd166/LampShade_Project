using System.Collections.Generic;
using BolgManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace ServiceHost.Areas.Administrator.Pages.Blog.ArticleCategories
{
    public class IndexModel : PageModel
    {

        private readonly IArticleCategoryApplication _articleCategoryApplication;

        public ArticleCategorySearchModel SearchModel;
        public List<ArticleCategoryViewModel> articleCategories;
        public IndexModel(IArticleCategoryApplication articleCategoryApplication)
        {
            _articleCategoryApplication = articleCategoryApplication;
        }

        public void OnGet(ArticleCategorySearchModel searchModel)
        {
            articleCategories = _articleCategoryApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create",new CreateArticleCategory());
        }
        public JsonResult OnPostCreate(CreateArticleCategory command)
        {
            var result = _articleCategoryApplication.CreateArticleCategory(command);
            return new JsonResult(result);
        }


        public IActionResult OnGetEdit(long id)
        {
            var ArticleCategory = _articleCategoryApplication.GetDetials(id);
            return Partial("./Edit", ArticleCategory);
        }

        public JsonResult OnPostEdit(EditArticleCategory command)
        {
            var result = _articleCategoryApplication.EditArticleCategory(command);
            return new JsonResult(result);

        }
    }
}
