using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.Slide
{
    public class CreateSlide
    {

        [FileExtensionLimitation(new string[] { ".jpeg", ".jpg", ".png" }, ErrorMessage = ValidationMessages.InvalidFileForamt)]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessages.OutOfRangeSize)]
        [Required(ErrorMessage =ValidationMessages.IsRequired)]
        public IFormFile Picture { get; set; }
        [Required(ErrorMessage =ValidationMessages.IsRequired)]
        public string PictureAlt { get; set; }

        public string PictureTitle { get; set; }
        [Required(ErrorMessage =ValidationMessages.IsRequired)]
        public string Heading { get; set; }
        [Required(ErrorMessage =ValidationMessages.IsRequired)]
        public string Title { get; set; }

        public string Text { get; set; }
        [Required(ErrorMessage =ValidationMessages.IsRequired)]
        public string BtnText { get; set; }

        public string Link { get; set; }
    }

   


}
