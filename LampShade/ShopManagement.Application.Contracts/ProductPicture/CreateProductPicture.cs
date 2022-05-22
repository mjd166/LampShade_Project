using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contracts.Product;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.ProductPicture
{
    public class CreateProductPicture
    {
        [Range(1,1000000,ErrorMessage =ValidationMessages.IsRequired)]
        public long ProductId { get; set; }
       // [Required(ErrorMessage =ValidationMessages.IsRequired)]
        [FileExtensionLimitation(new string[] { ".jpeg", ".jpg", ".png" }, ErrorMessage = ValidationMessages.InvalidFileForamt)]
        [MaxFileSize(1 * 1024 * 1024, ErrorMessage = ValidationMessages.OutOfRangeSize)]
        public IFormFile Picture { get; set; }
        [Required(ErrorMessage =ValidationMessages.IsRequired)]
        public string PictureAlt { get; set; }
        [Required(ErrorMessage =ValidationMessages.IsRequired)]
        public string PictureTitle { get; set; }
        public List<ProductViewModel>  Products { get; set; }

    }
}
