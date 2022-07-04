using EndPoint.Site.Utilites;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Services.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ICookiesManager _cookiesManager;

        public CartController(ICartService cartService, ICookiesManager cookiesManager)
        {
            _cartService = cartService;
            _cookiesManager = cookiesManager;
        }

        public IActionResult Index()
        {
            var userId = ClaimUtility.GetUserId(User);
            var result = _cartService.GetMyCart(_cookiesManager.GetBrowserId(HttpContext), userId);

            return View(result.Data);
        }

        public IActionResult AddItemToCart(long id)
        {
            _cartService.AddItemToCart(_cookiesManager.GetBrowserId(HttpContext), id);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveItemFromCart(long id)
        {
            _cartService.RemoveItemFromCart(_cookiesManager.GetBrowserId(HttpContext), id);
            return RedirectToAction("Index");
        }

        public IActionResult LowOffCartItem(long cartItemId)
        {
            _cartService.LowOffCartItem(cartItemId);
            return RedirectToAction("Index");
        }
        public IActionResult AddCartItem(long cartItemId)
        {
            _cartService.AddCartItem(cartItemId);
            return RedirectToAction("Index");
        }
    }
}
