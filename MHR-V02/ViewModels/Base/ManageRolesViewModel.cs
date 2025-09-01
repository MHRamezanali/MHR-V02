using Microsoft.AspNetCore.Mvc;

namespace MHR_V02.ViewModels.Base
{
    public class ManageRolesViewModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }

        [BindProperty]
        public List<RoleViewModel> Roles { get; set; } = new List<RoleViewModel>();
    }

}
