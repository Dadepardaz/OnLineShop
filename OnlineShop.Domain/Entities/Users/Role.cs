using OnlineShop.Domain.Entities.Common;
using System.Collections.Generic;

namespace OnlineShop.Domain.Entities.Users
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<UserInRole> UserInRoles { get; set; }
    }
}
