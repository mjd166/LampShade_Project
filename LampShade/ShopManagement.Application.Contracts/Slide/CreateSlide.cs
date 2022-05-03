using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.Slide
{
    public class CreateSlide
    {

        [Required(ErrorMessage =ValidationMessages.IsRequired)]
        public string Picture { get; set; }
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
