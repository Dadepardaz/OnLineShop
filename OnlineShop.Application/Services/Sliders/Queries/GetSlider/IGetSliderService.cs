using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Common;
using OnlineShop.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services.Sliders.Queries.GetSlider
{
    public interface IGetSliderService
    {
        ResultDto<ResultGetSliderDto> Execute(int page = 1, int pageSize = 20);
    }
    public class GetSliderService : IGetSliderService
    {
        private readonly IDataBaseContext _context;

        public GetSliderService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetSliderDto> Execute(int page = 1, int pageSize = 20)
        {
            int rowCount = 0;
            var sliders = _context.Sliders.ToPaged(page, pageSize, out rowCount)
                .Select(p => new GetSliderDto
                {
                    Id = p.Id,
                    Link = p.Link
                }).ToList();

            if (sliders == null)
            {
                return new ResultDto<ResultGetSliderDto>
                {
                    IsSuccess = false,
                    Message = "not found"
                };
            }

            return new ResultDto<ResultGetSliderDto>()
            {
                Data = new ResultGetSliderDto
                {
                    Page = page,
                    PageSize = pageSize,
                    RowCount = rowCount,
                    Sliders = sliders
                },
                IsSuccess = true,
                Message = ""
            };
        }
    }
    public class ResultGetSliderDto
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }
        public List<GetSliderDto> Sliders { get; set; }
    }
    public class GetSliderDto
    {
        public long Id { get; set; }
        public string Link { get; set; }
    }
}
