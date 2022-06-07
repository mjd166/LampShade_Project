using _01_LampshadeQuery.Contracts.Article;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class LatestBlogPostViewComponent :ViewComponent
    {
        private readonly IArticleQuery _articleQuery;

        public LatestBlogPostViewComponent(IArticleQuery articleQuery)
        {
            _articleQuery = articleQuery;
        }

        public  IViewComponentResult Invoke()
        {
            var articles = _articleQuery.GetLatestArticles();
            return View(articles);
        }
    }
}
