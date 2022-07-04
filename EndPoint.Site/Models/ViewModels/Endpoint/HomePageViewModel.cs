using OnlineShop.Application.Services.HomePage;
using OnlineShop.Application.Services.HomePage.ImagesInIndex;
using OnlineShop.Application.Services.HomePage.Products.GetCategoryAndProduct;
using OnlineShop.Application.Services.HomePage.Products.GetProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndPoint.Site.Models.ViewModels.Endpoint
{
    public class HomePageViewModel
    {
        public List<GetSliderDto> Sliders { get; set; }
        public List<CategoryAndCountDto> CategoryProduct { get; set; }
        public List<GetProductDto> TrandyProducts { get; set; }

        public List<ResulttHomePageImageDto> HomePageImage { get; set; }
    }
}
