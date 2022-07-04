using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services.HomePage
{
    public interface IGetslider
    {
        ResultDto<List<GetSliderDto>> Execute();
    }
    public class Getslider : IGetslider
    {
        private readonly IDataBaseContext _context;

        public Getslider(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<GetSliderDto>> Execute()
        {
            var sliders = _context.Sliders.OrderByDescending(p => p.Id).Take(3).ToList().Select(p => new GetSliderDto 
            { 
                Id = p.Id,
                Src = p.Src,
                Link = p.Link
            }).ToList();

            return new ResultDto<List<GetSliderDto>>()
            {
                Data = sliders,
                IsSuccess = true,
                Message = ""
            };
        }
    }
    public class GetSliderDto
    {
        public long Id { get; set; }
        public string Link { get; set; }
        public string Src { get; set; }
    }
}
