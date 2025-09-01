using MHR_V02.Models.Base;

namespace MHR_V02.Models.BasicTables
{
    public class UserAction
    {
        // اختصاص اکشن به کاربر
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid ActionLogId { get; set; }
        public ActionLog ActionLog { get; set; }

        public override bool Equals(object obj)
        {
            return obj is UserAction action &&
                   UserId == action.UserId &&
                   ActionLogId == action.ActionLogId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(UserId, ActionLogId);
        }

    }
}
