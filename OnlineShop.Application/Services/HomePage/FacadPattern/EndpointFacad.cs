using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Application.Interfaces.FacadPattern;
using OnlineShop.Application.Services.HomePage.ImagesInIndex;
using OnlineShop.Application.Services.HomePage.Products.DetailProduct;
using OnlineShop.Application.Services.HomePage.Products.GetCategoryAndProduct;
using OnlineShop.Application.Services.HomePage.Products.GetProduct;
using OnlineShop.Common.UploadFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services.HomePage.FacadPattern
{
    public class EndpointFacad : IEndpointFacad
    {
        private readonly IDataBaseContext _context;
        private readonly IUploadFileService _uploadFile;

        public EndpointFacad(IDataBaseContext context, IUploadFileService uploadFile)
        {
            _context = context;
            _uploadFile = uploadFile;
        }


        private IGetslider _getslider;
        public IGetslider GetSlider 
        {
            get
            {
                return _getslider = _getslider ?? new Getslider(_context);
            }
        }

        private IGetCategoryAndProduct _getCategoryAndProduct;
        public IGetCategoryAndProduct GetCategoryAndProduct
        {
            get
            {
                return _getCategoryAndProduct = _getCategoryAndProduct ?? new GetCategoryAndProduct(_context);
            }
        }

        private IGetProductForIndex _getProductForIndex;
        public IGetProductForIndex GetProductForIndex
        {
            get
            {
                return _getProductForIndex = _getProductForIndex ?? new GetProductForIndex(_context);
            }
        }

        private IDetailProductService _detailProductService;
        public IDetailProductService DetailProductService 
        {
            get
            {
                return _detailProductService = _detailProductService ?? new DetailProductService(_context);
            }
        }

        private IAddHomePgeImage _addHomePgeImage;

      

        public IAddHomePgeImage AddHomePgeImage
        {
            get
            {
                return _addHomePgeImage = _addHomePgeImage ?? new AddHomePgeImage(_context, _uploadFile);
            }
        }

        private IGetImageHome _getImageHome;
        public IGetImageHome GetImageHome 
        {
            get
            {
                return _getImageHome = _getImageHome ?? new GetImageHome(_context);
            }
        }
    }
}
