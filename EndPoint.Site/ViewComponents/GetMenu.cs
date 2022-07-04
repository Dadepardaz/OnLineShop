using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Services.HomePage.MenuItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndPoint.Site.ViewComponents
{
    public class GetMenu : ViewComponent
    {
        private readonly IGetMenuItem _getMenuItem;

        public GetMenu(IGetMenuItem getMenuItem)
        {
            _getMenuItem = getMenuItem;
        }

        public IViewComponentResult Invoke()
        {
            var menu = _getMenuItem.Execute().Data;
            return View(viewName: "GetMenu", menu);
        }
    }
}
