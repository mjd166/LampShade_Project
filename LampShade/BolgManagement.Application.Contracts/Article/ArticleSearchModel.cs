using BolgManagement.Application.Contracts.ArticleCategory;
using System.Collections.Generic;

namespace BolgManagement.Application.Contracts.Article
{
    public class ArticleSearchModel
    {
        public string Title { get; set; }
        public long CategoryId { get; set; }
       // public List<ArticleCategoryViewModel> Categories { get; set; }
    }
}
