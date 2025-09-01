using MHR_V02.Models.Base;
using Microsoft.AspNetCore.Mvc;

namespace MHR_V02.ViewModels.Base
{
    public class ManageActionLogsViewModel
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
      
        public int TotalCount { get; set; } // تعداد کل رکوردها
        public int PageNumber { get; set; } = 1; // شماره صفحه فعلی
        public int PageSize { get; set; } = 10; // تعداد آیتم‌های هر صفحه




        [BindProperty]
        public List<ActionLogViewModel> ActionLogs { get; set; } = new List<ActionLogViewModel>();
    }

}
