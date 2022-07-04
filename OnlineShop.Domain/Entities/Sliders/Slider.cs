using OnlineShop.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Domain.Entities.Sliders
{
    public class Slider :BaseEntity
    {
        public string Src { get; set; }
        public string Link { get; set; }
    }
}
