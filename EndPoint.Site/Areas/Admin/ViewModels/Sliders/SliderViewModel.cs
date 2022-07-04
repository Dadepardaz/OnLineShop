using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.ViewModels.Sliders
{
    public class SliderViewModel
    {
        public IFormFile Image { get; set; }
        public string Link { get; set; }
    }
}
