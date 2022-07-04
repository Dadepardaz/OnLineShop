using Microsoft.AspNetCore.Http;
using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Common.Dto;
using OnlineShop.Common.UploadFile;
using OnlineShop.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services.Products.Commands.AddProduct
{
    public interface IAddProductService
    {
        ResultDto Execute(AddProductDto request);
    }
    public class AddProductService : IAddProductService
    {
        private readonly IDataBaseContext _context;
        private readonly IUploadFileService _uploadFileService;

        public AddProductService(IDataBaseContext context, IUploadFileService uploadFileService)
        {
            _context = context;
            _uploadFileService = uploadFileService;
        }

        public ResultDto Execute(AddProductDto request)
        {
            if (string.IsNullOrEmpty(request.Title))
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "Enter Title"

                };
            }
            try
            {
                var category = _context.Categories.Find(request.CategoryId);
                if (category == null)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "Category not found"

                    };
                }

          
                Product product = new Product()
                {
                    Title = request.Title,
                    Brand = request.Brand,
                    Price = request.Price,
                    Discription = request.Discription,
                    IsDisplayed = request.IsDisplayed,
                    Inventory = request.Inventory,
                    Category = category
                };

                _context.Products.Add(product);

                List<ProductsImages> imagesProduct = new List<ProductsImages>();
                foreach (var item in request.Images)
                {
                 var resultupload =  _uploadFileService.ExecuteFileUpload(item);
                    imagesProduct.Add(new ProductsImages
                    {
                        Product = product,
                        Src = resultupload.AddressFileName
                    });
                }

                _context.ProductsImages.AddRange(imagesProduct);

                List<ProductFeatures> productFeatures = new List<ProductFeatures>();
                foreach (var item in request.FeatureProduct)
                {
                    productFeatures.Add(new ProductFeatures
                    {
                        DisplayName = item.DisplayName,
                        Value = item.Value,
                        Product = product
                    });
                }
                _context.ProductFeatures.AddRange(productFeatures);

                _context.SaveChanges();

                return new ResultDto()
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
                    Message = "Error"

                };
            }
        }
    }
    public class AddProductDto
    {
        public string Title { get; set; }
        public string Brand { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public bool IsDisplayed { get; set; }
        public string Discription { get; set; }
        public long CategoryId { get; set; }
        public List<IFormFile> Images { get; set; }
        public List<FeatureProductDto> FeatureProduct{ get; set; }

    }

    public class FeatureProductDto
    {
        public string DisplayName { get; set; }
        public string Value { get; set; }
    }
}
