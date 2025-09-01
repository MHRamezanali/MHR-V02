using MHR_V02.Models.Base;

namespace MHR_V02.Models.BasicTables
{
    public class RoleAction
    {
        // اختصاص اکشن به نقش
        public Guid RoleId { get; set; }
        public Role Role { get; set; }

        public Guid ActionLogId { get; set; }
        public ActionLog ActionLog { get; set; }

        public override bool Equals(object obj)
        {
            return obj is RoleAction action &&
                   RoleId == action.RoleId &&
                   ActionLogId == action.ActionLogId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(RoleId, ActionLogId);
        }

    }
}
