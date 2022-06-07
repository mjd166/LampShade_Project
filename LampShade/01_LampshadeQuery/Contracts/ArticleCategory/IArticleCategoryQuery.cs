using System.Collections.Generic;

namespace _01_LampshadeQuery.Contracts.ArticleCategory
{
    public interface IArticleCategoryQuery
    {
        ArticleCategoryQueryModel GetArticleCategories(string slug);
        List<ArticleCategoryQueryModel> GetArticleCategoris();
    }
}
