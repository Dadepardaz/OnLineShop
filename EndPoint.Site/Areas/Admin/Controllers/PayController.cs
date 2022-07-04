using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Services.Finances.Queries.GetPayForAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize]
    public class PayController : Controller
    {
        private readonly IGetPayForAdmin _getPayForAdmin;

        public PayController(IGetPayForAdmin getPayForAdmin)
        {
            _getPayForAdmin = getPayForAdmin;
        }

        public IActionResult Index(int page = 1, int pageSize = 20)
        {
            return View(_getPayForAdmin.Execute(page, pageSize).Data);
        }
    }
}
