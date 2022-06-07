using _0_Framework.Application;
using _01_LampshadeQuery.Contracts.Article;
using _01_LampshadeQuery.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _01_LampshadeQuery.Query
{
    public class ArticleCategoryQuery : IArticleCategoryQuery
    {
        private readonly BlogContext _blogContext;

        public ArticleCategoryQuery(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public ArticleCategoryQueryModel GetArticleCategories(string slug)
        {
            return _blogContext.ArticleCategories.Select(x => new ArticleCategoryQueryModel
            {
                 Name=x.Name,
                 Description=x.Description,
                 Picture=x.Picture,
                 PictureAlt=x.PictureAlt,
                 PictureTitle=x.PictureTitle,
                 Slug=x.Slug,
                 Keywords=x.Keywords,
                 MetaDescription=x.MetaDescription,
                 CanonicalAddress =x.CanonicalAddress,
                 Articles=MapArticles(x.Articles)

            }).FirstOrDefault(x=>x.Slug==slug);
        }

        private static List<ArticleQueryModel> MapArticles(List<Article> articles)
        {
            return articles.Select(x => new ArticleQueryModel
            {
                Slug=x.Slug,
                ShortDescription=x.ShortDescription,
                Title =x.Title,
                Picture=x.Picture,
                PictureAlt=x.PictureAlt,
                PictureTitle=x.PictureTitle,
                 PublishDate =x.PublishDate.ToFarsi()
            }).ToList();
        }

        public List<ArticleCategoryQueryModel> GetArticleCategoris()
        {
            return _blogContext.ArticleCategories.Include(x => x.Articles)
                .Select(x => new ArticleCategoryQueryModel
                {
                    Name = x.Name,
                    Description = x.Description,
                    ShowOrder = x.ShowOrder,
                    Slug = x.Slug,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                   
                    ArticlesCount = x.Articles.Count
                }).AsNoTracking().ToList();
        }
    }
}
