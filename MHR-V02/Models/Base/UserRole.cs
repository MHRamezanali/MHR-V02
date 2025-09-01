using MHR_V02.Models.Base;

namespace MHR_V02.Models.BasicTables
{
    public class UserRole
    {
        // اختصاص نقش به کاربر
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid RoleId { get; set; }
        public Role Role { get; set; }

        public override bool Equals(object obj)
        {
            return obj is UserRole role &&
                   UserId == role.UserId &&
                   RoleId == role.RoleId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(UserId, RoleId);
        }
    }
}
