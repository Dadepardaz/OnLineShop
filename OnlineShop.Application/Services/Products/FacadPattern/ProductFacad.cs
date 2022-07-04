using Microsoft.AspNetCore.Hosting;
using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Application.Interfaces.FacadPattern;
using OnlineShop.Application.Services.Products.Commands.AddNewCategory;
using OnlineShop.Application.Services.Products.Commands.AddProduct;
using OnlineShop.Application.Services.Products.Commands.DeleteProduct;
using OnlineShop.Application.Services.Products.Commands.EditProduct;
using OnlineShop.Application.Services.Products.Commands.RemoveCategory;
using OnlineShop.Application.Services.Products.Queries.GetCategoriesForProduct;
using OnlineShop.Application.Services.Products.Queries.GetCategory;
using OnlineShop.Application.Services.Products.Queries.GetProductDetailForAdmin;
using OnlineShop.Application.Services.Products.Queries.GetProductForAdmin;
using OnlineShop.Common.UploadFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services.Products.FacadPattern
{
    public class ProductFacad : IProductFacad
    {
        private readonly IDataBaseContext _context;
        private readonly IUploadFileService _uploadFile;


        public ProductFacad(IDataBaseContext context, IUploadFileService uploadFile)
        {
            _context = context;
            _uploadFile = uploadFile;
        }


        private IAddNewCategory _addNewCategory;
        public IAddNewCategory AddNewCategory
        {
            get
            {
                return _addNewCategory = _addNewCategory ?? new AddNewCategory(_context);
            }
        }

        private IGetCategoryService _getCategoryService;
        public IGetCategoryService GetCategoryService
        {
            get
            {
                return _getCategoryService = _getCategoryService ?? new GetCategoryService(_context);
            }
        }

        private IRemoveCategoryService _removeCategoryService;
        public IRemoveCategoryService RemoveCategoryService
        {
            get
            {
                return _removeCategoryService = _removeCategoryService ?? new RemoveCategoryService(_context);
            }
        }

        private IAddProductService _addProductService;


        public IAddProductService AddProductService
        {
            get
            {
                return _addProductService = _addProductService ?? new AddProductService(_context, _uploadFile);
            }
        }

        private IGetCategoriesForProductService _getCategoriesForProductService;
        public IGetCategoriesForProductService GetCategoriesForProductService
        {
            get
            {
                return _getCategoriesForProductService = _getCategoriesForProductService ?? new GetCategoriesForProductService(_context);
            }
        }

        private IGetProductForAdminService _getProductForAdminService;
        public IGetProductForAdminService GetProductForAdminService
        {
            get
            {
                return _getProductForAdminService = _getProductForAdminService ?? new GetProductForAdminService(_context);
            }
        }

        private IDeleteProductService _deleteProductService;
        public IDeleteProductService DeleteProductService
        {
            get
            {
                return _deleteProductService = _deleteProductService ?? new DeleteProductService(_context);
            }
        }

        private IGetProductDetailForAdminService _getProductDetailForAdminService;
        public IGetProductDetailForAdminService GetProductDetailForAdminService
        {
            get
            {
                return _getProductDetailForAdminService = _getProductDetailForAdminService ?? new GetProductDetailForAdminService(_context);
            }
        }

        private IEditProductService _editProductService;
        public IEditProductService EditProductService
        {
            get
            {
                return _editProductService = _editProductService ?? new EditProductService(_context);
            }
        }
    }
}
