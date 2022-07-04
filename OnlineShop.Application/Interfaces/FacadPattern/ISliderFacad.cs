using OnlineShop.Application.Services.Sliders.Command.AddSlider;
using OnlineShop.Application.Services.Sliders.Command.RemoveSlider;
using OnlineShop.Application.Services.Sliders.Queries.DetailSlider;
using OnlineShop.Application.Services.Sliders.Queries.GetSlider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Interfaces.FacadPattern
{
    public interface ISliderFacad
    {
        IAddSliderService AddSliderService { get; }
        IGetSliderService GetSliderService { get; }
        IRemoveSliderservice RemoveSliderservice { get; }
        IDetailSliderService DetailSliderService { get; }
    }
}
