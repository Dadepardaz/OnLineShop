using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Common.Dto;
using OnlineShop.Common.UploadFile;
using OnlineShop.Domain.Entities.Sliders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services.Sliders.Command.AddSlider
{
    public interface IAddSliderService
    {
        ResultDto Execute(AddSliderDto request);
    }
    public class AddSliderService : IAddSliderService
    {
        private readonly IDataBaseContext _context;
        private readonly IUploadFileService _uploadFileService;

        public AddSliderService(IDataBaseContext context, IUploadFileService uploadFileService)
        {
            _context = context;
            _uploadFileService = uploadFileService;
        }

        public ResultDto Execute(AddSliderDto request)
        {
            if (request.Image == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "select a image"
                };
            }
            try
            {
                var reaultUpload = _uploadFileService.ExecuteFileUpload(request.Image);
                Slider slider = new Slider()
                {
                    Src = reaultUpload.AddressFileName,
                    Link = request.Link
                };
                _context.Sliders.Add(slider);
                _context.SaveChanges();

                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "Success"
                };
            }
            catch (Exception)
            {

                return new ResultDto
                {
                    IsSuccess = false,
                    Message = ""
                };
            }


        }
    }
    public class AddSliderDto
    {
        public IFormFile Image { get; set; }
        public string Link { get; set; }
    }


}
