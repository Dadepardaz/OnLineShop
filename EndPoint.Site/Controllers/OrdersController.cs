using EndPoint.Site.Utilites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Services.Orders.Queries.GetOrderForUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IGetOrderForUserService _getOrderForUserService;

        public OrdersController(IGetOrderForUserService getOrderForUserService)
        {
            _getOrderForUserService = getOrderForUserService;
        }

        public IActionResult Index()
        {
            long userId = ClaimUtility.GetUserId(User).Value;
            return View(_getOrderForUserService.Execute(userId).Data);
        }
    }
}
