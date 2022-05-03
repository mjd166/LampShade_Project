using _01_LampshadeQuery.Contracts.Slide;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class SlideViewComponent :ViewComponent
    {
        private readonly ISlideQuery _sliderQuery;

        public SlideViewComponent(ISlideQuery sliderQuery)
        {
            _sliderQuery = sliderQuery;
        }

        public IViewComponentResult Invoke()
        {
            var slides = _sliderQuery.GetSlides();
            return View(slides);
        }
    }
}
