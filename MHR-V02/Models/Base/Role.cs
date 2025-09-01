using MHR_V02.Models.Base;
using MHR_V02.Models.BasicTables;

namespace MHR_V02.Models.Base
{
    public class Role : BaseEntity
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
            RoleActions = new HashSet<RoleAction>();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<RoleAction> RoleActions { get; set; }
    }
}
