using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Services.Orders.Queries.GetOrderForAdmin;
using OnlineShop.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly IGetOrderForAdminService _getOrderForAdmin;

        public OrdersController(IGetOrderForAdminService getOrderForAdmin)
        {
            _getOrderForAdmin = getOrderForAdmin;
        }

        public IActionResult Index(OrderStatus orderStatus, int page = 1, int pageSize = 20)
        {

            return View(_getOrderForAdmin.Execute(orderStatus, page, pageSize).Data);
        }
    }
}
