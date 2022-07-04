using EndPoint.Site.Models;
using EndPoint.Site.Models.ViewModels.Endpoint;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineShop.Application.Interfaces.FacadPattern;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace EndPoint.Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEndpointFacad _endpointFacad;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IEndpointFacad endpointFacad, ILogger<HomeController> logger)
        {
            _endpointFacad = endpointFacad;
            _logger = logger;
        }

        public IActionResult Index()
        {
            HomePageViewModel model = new HomePageViewModel
            {
                Sliders = _endpointFacad.GetSlider.Execute().Data,
                CategoryProduct = _endpointFacad.GetCategoryAndProduct.Execute().Data,
                TrandyProducts = _endpointFacad.GetProductForIndex.Execute().Data,
                HomePageImage = _endpointFacad.GetImageHome.Execute().Data

            };
            return View(model);
        }

        public IActionResult Detail(long id)
        {
            return View(_endpointFacad.DetailProductService.Execute(id).Data);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

      

    }
}
