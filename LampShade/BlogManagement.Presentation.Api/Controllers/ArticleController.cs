using _01_LampshadeQuery.Contracts.Article;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogManagement.Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleQuery _articleQuery;

        public ArticleController(IArticleQuery articleQuery)
        {
            _articleQuery = articleQuery;
        }

        [HttpGet]
        public List<ArticleQueryModel> GetLatestArticles()
        {
            return _articleQuery.GetLatestArticles();
        }
    }
}
