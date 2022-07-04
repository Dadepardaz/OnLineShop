using Microsoft.AspNetCore.Http;
using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Common.Dto;
using OnlineShop.Common.UploadFile;
using OnlineShop.Domain.Entities.HomePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services.HomePage.ImagesInIndex
{
    public interface IAddHomePgeImage
    {
        ResultDto Execute(RequestHomePageImageDto request);
    }

    public class AddHomePgeImage : IAddHomePgeImage
    {
        private readonly IDataBaseContext _context;
        private readonly IUploadFileService _uploadFileService;

        public AddHomePgeImage(IDataBaseContext context, IUploadFileService uploadFileService)
        {
            _context = context;
            _uploadFileService = uploadFileService;
        }

        public ResultDto Execute(RequestHomePageImageDto request)
        {
            var resultUpload = _uploadFileService.ExecuteFileUpload(request.Src);
            _context.HomePageImages.Add(new HomePageImage
            {
                Src = resultUpload.AddressFileName,
                Link = request.Link,
                ImageLocation = request.ImageLocation
            });
            _context.SaveChanges();

            return new ResultDto
            {
                IsSuccess = true,
                Message =""
            };
        }

    }
    public class RequestHomePageImageDto 
    {
        public IFormFile Src { get; set; }
        public string Link { get; set; }
        public ImageLocation ImageLocation { get; set; }
    }
}
