using EndPoint.Site.Areas.Admin.ViewModels.Sliders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Interfaces.FacadPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly ISliderFacad _sliderFacad;

        public SlidersController(ISliderFacad sliderFacad)
        {
            _sliderFacad = sliderFacad;
        }

        public IActionResult Index(int page = 1, int pageSize = 20)
        {
            return View(_sliderFacad.GetSliderService.Execute(page, pageSize).Data);
        }

        public IActionResult AddSlider()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddSlider(OnlineShop.Application.Services.Sliders.Command.AddSlider.AddSliderDto model)
        {
            var image = Request.Form.Files[0];

            model.Image = image;
            var result = _sliderFacad.AddSliderService.Execute(model);
         
            return Json(result);
        }

        [HttpPost]
        public IActionResult Delete(long sliderId)
        {
            return Json(_sliderFacad.RemoveSliderservice.Execute(sliderId));
        }

       
        public IActionResult Detail(long id)
        {
            return View(_sliderFacad.DetailSliderService.Execute(id).Data);
        }
    }
}
