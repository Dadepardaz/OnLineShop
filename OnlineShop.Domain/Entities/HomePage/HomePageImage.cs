using OnlineShop.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Domain.Entities.HomePage
{
    public class HomePageImage : BaseEntity
    {
        public string Src { get; set; }
        public string Link { get; set; }
        public ImageLocation ImageLocation { get; set; }
    }

    public enum ImageLocation 
    {
        LeftImage =0,
        RightImage = 1
    }

}
