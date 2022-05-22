using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.ProductCategory
{
    public class CreateProductCategory
    {
        [Required(ErrorMessage =ValidationMessages.IsRequired)]
        public string Name { get;  set; }
        public string Description { get;  set; }
        //[Required(ErrorMessage =ValidationMessages.IsRequired)]
        [FileExtensionLimitation(new string[] {".jpeg",".jpg",".png"},ErrorMessage =ValidationMessages.InvalidFileForamt )]
        [MaxFileSize(3*1024*1024,ErrorMessage =ValidationMessages.OutOfRangeSize)]
        public IFormFile Picture { get;  set; }
        public string PictureAlt { get;  set; }
        public string PictureTitle { get;  set; }


        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Keywords { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Metadescription { get; set; }


        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Slug { get;  set; }
    }
}
