﻿using _0_Framework.Domain;
using ShopManagement.Domain.ProductAgg;
using System;
using System.Collections.Generic;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public class ProductCategory : EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string Keywords { get; private set; }
        public string Metadescription { get; set; }
        public string Slug { get; private set; }

        //Navigation Property
        public List<Product> Products { get; private set; }


        public ProductCategory()
        {
            this.Products = new List<Product>();
        }

        public ProductCategory(string name, string description, string picture, string pictureAlt, string pictureTitle, string keywords, string metadescription, string slug)
        {
            Name = name;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Keywords = keywords;
            Metadescription = metadescription;
            Slug = slug;

        }

        public void Edit(string name, string description, string picture, string pictureAlt, string pictureTitle, string keywords, string metadescription, string slug)
        {
            Name = name;
            Description = description;
            if (!string.IsNullOrWhiteSpace(picture))
                Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Keywords = keywords;
            Metadescription = metadescription;
            Slug = slug;
        }
    }
}
