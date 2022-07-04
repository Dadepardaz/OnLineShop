using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services.Sliders.Queries.DetailSlider
{
    public interface IDetailSliderService
    {
        ResultDto<DetailSliderDto> Execute(long sliderId);
    }
    public class DetailSliderService : IDetailSliderService
    {
        private readonly IDataBaseContext _context;

        public DetailSliderService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<DetailSliderDto> Execute(long sliderId)
        {
            var slider = _context.Sliders.Where(p => p.Id == sliderId).Select(p => new DetailSliderDto
            {
                Link = p.Link
            }).FirstOrDefault();

            if (slider == null)
            {
                return new ResultDto<DetailSliderDto>
                {
                    IsSuccess = false,
                    Message = ""
                };
            }

            return new ResultDto<DetailSliderDto>()
            {
                Data = slider,
                IsSuccess = true,
                Message = ""
            };
        }
    }
    public class DetailSliderDto
    {
        public string Link { get; set; }
    }
}
