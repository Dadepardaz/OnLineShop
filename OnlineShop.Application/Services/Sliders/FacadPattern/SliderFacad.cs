using Microsoft.AspNetCore.Hosting;
using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Application.Interfaces.FacadPattern;
using OnlineShop.Application.Services.Sliders.Command.AddSlider;
using OnlineShop.Application.Services.Sliders.Command.RemoveSlider;
using OnlineShop.Application.Services.Sliders.Queries.DetailSlider;
using OnlineShop.Application.Services.Sliders.Queries.GetSlider;
using OnlineShop.Common.UploadFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services.Sliders.FacadPattern
{
    public class SliderFacad : ISliderFacad
    {
        private readonly IDataBaseContext _context;
        private readonly IUploadFileService _uploadFileService;

        public SliderFacad(IDataBaseContext context, IUploadFileService uploadFileService)
        {
            _context = context;
            _uploadFileService = uploadFileService;
        }


        private IAddSliderService _addSliderService;
        public IAddSliderService AddSliderService
        {
            get
            {
                return _addSliderService = _addSliderService ?? new AddSliderService(_context, _uploadFileService);
            }
        }

        private IGetSliderService _getSliderService;
        public IGetSliderService GetSliderService
        {
            get
            {
                return _getSliderService = _getSliderService ?? new GetSliderService(_context);
            }
        }

        private IRemoveSliderservice _removeSliderservice;
        public IRemoveSliderservice RemoveSliderservice 
        {
            get
            {
                return _removeSliderservice = _removeSliderservice ?? new RemoveSliderservice(_context);
            }
        }

        private IDetailSliderService _detailSliderService;
        public IDetailSliderService DetailSliderService 
        {
            get
            {
                return _detailSliderService = _detailSliderService ?? new DetailSliderService(_context);
            }
        }
    }
}
