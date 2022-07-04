using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.ViewModels.Category
{
    public class CategoryViewModel
    {
        public string Title { get; set; }
        public long? ParentId { get; set; }
    }
}
