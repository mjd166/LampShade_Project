using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Application.Contracts.Slide;

namespace ServiceHost.Areas.Administrator.Pages.Shop.Slides
{
    public class IndexModel : PageModel
    {

        [TempData]
        public string Message { get; set; }

        private readonly ISlideApplication _slideApplication;

        public List<SlideViewModel> Slides { get; set; }

        public IndexModel(ISlideApplication slideApplication)
        {
            _slideApplication = slideApplication;
        }

        public void OnGet()
        {

            Slides = _slideApplication.GetList() ;
        }

        public IActionResult OnGetCreate()
        {
           
            return Partial("Create", new CreateSlide());
        }

        public JsonResult OnPostCreate(CreateSlide command)
        {
            return new JsonResult(_slideApplication.Create(command));
        }

        public IActionResult OnGetEdit(long id)
        {
            var Slide = _slideApplication.GetDetails(id);
            
            return Partial("Edit", Slide);
        }

        public JsonResult OnPostEdit(EditSlide command)
        {
            return new JsonResult(_slideApplication.Edit(command));
        }


        public IActionResult OnGetRemove(long id)
        {
            var result = _slideApplication.Remove(id);
            if (result.IsSuccedded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetRestore(long id)
        {
            var result = _slideApplication.Restore(id);
            if (result.IsSuccedded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
