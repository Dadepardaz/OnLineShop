using EndPoint.Site.Areas.Admin.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Interfaces.FacadPattern;
using OnlineShop.Application.Services.Products.Commands.AddNewCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly IProductFacad _productFacad;

        public CategoriesController(IProductFacad productFacad)
        {
            _productFacad = productFacad;
        }

        public IActionResult Index(long? parentId)
        {
            return View(_productFacad.GetCategoryService.Execute(parentId).Data);
        }

        public IActionResult AddCategory(long? parentId)
        {
            //ViewBag.ParentId = parentId;

            return View(new CategoryViewModel { 
                ParentId = parentId
            });
        }

        [HttpPost]
        public IActionResult AddCategory(CategoryViewModel model)
        {
            return Json(_productFacad.AddNewCategory.Execute(new AddNewCategoryDto
            {
                Title = model.Title,
                ParentId = model.ParentId
            }));

        }

        [HttpPost]
        public IActionResult Delete(long? categoryId)
        {
            return Json(_productFacad.RemoveCategoryService.Execute(categoryId));
        }
    }
}
