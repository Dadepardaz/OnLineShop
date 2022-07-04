using OnlineShop.Application.Services.HomePage;
using OnlineShop.Application.Services.HomePage.ImagesInIndex;
using OnlineShop.Application.Services.HomePage.Products.DetailProduct;
using OnlineShop.Application.Services.HomePage.Products.GetCategoryAndProduct;
using OnlineShop.Application.Services.HomePage.Products.GetProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Interfaces.FacadPattern
{
    public interface IEndpointFacad
    {
        IGetslider GetSlider { get; }
        IGetCategoryAndProduct GetCategoryAndProduct { get; }
        IGetProductForIndex GetProductForIndex { get; }
        IDetailProductService DetailProductService { get; }
        IAddHomePgeImage AddHomePgeImage { get; }
        IGetImageHome GetImageHome { get; }
    }
}
