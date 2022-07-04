using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services.Sliders.Command.RemoveSlider
{
    public interface IRemoveSliderservice
    {
        ResultDto Execute(long sliderId);
    }
    public class RemoveSliderservice : IRemoveSliderservice
    {
        private readonly IDataBaseContext _context;

        public RemoveSliderservice(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long sliderId)
        {
            var slider = _context.Sliders.Find(sliderId);
            if (slider == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "not found"
                };
            }

            slider.IsRemoved = true;
            slider.RemoveTime = DateTime.Now;

            return new ResultDto
            {
                IsSuccess = true,
                Message = "Success"
            };
        }
    }
}
