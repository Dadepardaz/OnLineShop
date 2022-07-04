using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Common.Dto;
using OnlineShop.Domain.Entities.HomePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services.HomePage.ImagesInIndex
{
    public interface IGetImageHome
    {
        ResultDto<List<ResulttHomePageImageDto>> Execute();
    }

    public class GetImageHome : IGetImageHome
    {
        private readonly IDataBaseContext _context;

        public GetImageHome(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<ResulttHomePageImageDto>> Execute()
        {
            var imagesHome = _context.HomePageImages.OrderByDescending(i => i.Id).Select(i => new ResulttHomePageImageDto
            {
                Id = i.Id,
                Src = i.Src,
                Link = i.Link,
                ImageLocation = i.ImageLocation
            }).ToList();

            return new ResultDto<List<ResulttHomePageImageDto>>()
            {
                Data = imagesHome,
                IsSuccess = true,
                Message = ""
            };
        }
    }
    public class ResulttHomePageImageDto
    {
        public long Id { get; set; }
        public string Src { get; set; }
        public string Link { get; set; }
        public ImageLocation ImageLocation { get; set; }
    }
}
