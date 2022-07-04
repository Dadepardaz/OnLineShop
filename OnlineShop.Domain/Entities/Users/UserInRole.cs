using OnlineShop.Domain.Entities.Common;

namespace OnlineShop.Domain.Entities.Users
{
    public class UserInRole : BaseEntity
    {
        public long RoleId { get; set; }
        public virtual Role Role { get; set; }

        public long UserId { get; set; }
        public virtual User User { get; set; }
    }
}
