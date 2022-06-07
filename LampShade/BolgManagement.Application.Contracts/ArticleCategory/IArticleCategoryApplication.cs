using _0_Framework.Application;
using System.Collections.Generic;

namespace BolgManagement.Application.Contracts.ArticleCategory
{
    public interface IArticleCategoryApplication
    {
        OperationResult CreateArticleCategory(CreateArticleCategory command);
        OperationResult EditArticleCategory(EditArticleCategory command);
        EditArticleCategory GetDetials(long id);
       
        List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);

        List<ArticleCategoryViewModel> GetArticleCategories();
    }

}
