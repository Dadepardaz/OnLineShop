using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Interfaces.FacadPattern;
using OnlineShop.Application.Services.HomePage.ImagesInIndex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomePageImagesController : Controller
    {
        private readonly IEndpointFacad _endpointFacad;

        public HomePageImagesController(IEndpointFacad endpointFacad)
        {
            _endpointFacad = endpointFacad;
        }

        public IActionResult Index()
        {
            return View(_endpointFacad.GetImageHome.Execute().Data);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(RequestHomePageImageDto model)
        {
            var image = Request.Form.Files[0];
            model.Src = image;
            var result = _endpointFacad.AddHomePgeImage.Execute(model);
            return Json(result);
        }
    }
}
