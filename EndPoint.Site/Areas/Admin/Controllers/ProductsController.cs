using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShop.Application.Interfaces.FacadPattern;
using OnlineShop.Application.Services.Products.Commands.AddProduct;
using OnlineShop.Application.Services.Products.Commands.EditProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductFacad _productFacad;

        public ProductsController(IProductFacad productFacad)
        {
            _productFacad = productFacad;
        }

        public IActionResult Index(int page = 1, int pageSize = 20)
        {
            return View(_productFacad.GetProductForAdminService.Execute(page, pageSize).Data);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewBag.Categories = new SelectList(_productFacad.GetCategoriesForProductService.Execute().Data, "Id", "Title");
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(AddProductDto model)
        {
            List<IFormFile> images = new List<IFormFile>();
            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                var file = Request.Form.Files[i];
                images.Add(file);
            }
            model.Images = images;
            return Json(_productFacad.AddProductService.Execute(model));
        }

        [HttpPost]
        public IActionResult Delete(long productId)
        {
            return Json(_productFacad.DeleteProductService.Execute(productId));
        }
      
        public IActionResult Detail(long id)
        {
            return View(_productFacad.GetProductDetailForAdminService.Execute(id).Data);
        }

        [HttpPost]
        public IActionResult EditProduct(long productId, string title)
        {
            return Json(_productFacad.EditProductService.Execute(new EditProductDto
            { 
                ProductId = productId,
                Title = title
            }));
        }
    }
}
