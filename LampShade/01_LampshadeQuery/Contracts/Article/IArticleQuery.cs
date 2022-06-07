using System.Collections.Generic;

namespace _01_LampshadeQuery.Contracts.Article
{
    public interface IArticleQuery
    {
        List<ArticleQueryModel> GetLatestArticles();

        ArticleQueryModel GetArticelDetails(string slug);
    }
}
