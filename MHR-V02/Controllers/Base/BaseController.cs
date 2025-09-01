using MHR_V02.Data;
using MHR_V02.Resources;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Resources;

public abstract class BaseController : Controller
{
    private readonly CultureInfo _culture;
    private readonly ApplicationDbContext _context;
    // سازنده کلاس پایه
    protected BaseController()
    {
    }
    // متد برای تنظیم ریسورس‌های عمومی
    protected void SetCommonLocalizedValues()
    {
        ViewBag.Title = BaseResources.ResourceManager.GetString("Title", _culture);

        ViewBag.Active = BaseResources.ResourceManager.GetString("Active", _culture);
        ViewBag.Inactive = BaseResources.ResourceManager.GetString("Inactive", _culture);


        ViewBag.Login = BaseResources.ResourceManager.GetString("Login", _culture);
        ViewBag.Logout = BaseResources.ResourceManager.GetString("Logout", _culture);
        ViewBag.Register = BaseResources.ResourceManager.GetString("Register", _culture);

        ViewBag.Create = BaseResources.ResourceManager.GetString("Create", _culture);
        ViewBag.Delete = BaseResources.ResourceManager.GetString("Delete", _culture);
        ViewBag.Details = BaseResources.ResourceManager.GetString("Details", _culture);
        ViewBag.Edit = BaseResources.ResourceManager.GetString("Edit", _culture);
        ViewBag.Description = BaseResources.ResourceManager.GetString("Description", _culture);

        ViewBag.Save = BaseResources.ResourceManager.GetString("Save", _culture);
        ViewBag.Password = BaseResources.ResourceManager.GetString("Password", _culture);
        ViewBag.Register = BaseResources.ResourceManager.GetString("Register", _culture);
        ViewBag.Email = BaseResources.ResourceManager.GetString("Email", _culture);
        ViewBag.RememberMe = BaseResources.ResourceManager.GetString("RememberMe", _culture);

        ViewBag.CreateNewUser = BaseResources.ResourceManager.GetString("CreateNewUser", _culture);
        ViewBag.EnterUseDetails = BaseResources.ResourceManager.GetString("EnterUseDetails", _culture);

        ViewBag.ActionName = BaseResources.ResourceManager.GetString("ActionName", _culture);
        ViewBag.ActiveStatus = BaseResources.ResourceManager.GetString("ActiveStatus", _culture);
        ViewBag.NoActionsFound = RolesResources.ResourceManager.GetString("NoActionsFound", _culture);



        ViewBag.Areyousureyouwanttodeletethis = BaseResources.ResourceManager.GetString("Areyousureyouwanttodeletethis", _culture);
        ViewBag.Assigned = BaseResources.ResourceManager.GetString("Assigned", _culture);
        ViewBag.BacktoList = BaseResources.ResourceManager.GetString("BacktoList", _culture);
        ViewBag.ConfirmPassword = BaseResources.ResourceManager.GetString("ConfirmPassword", _culture);
        ViewBag.ControllerName = BaseResources.ResourceManager.GetString("ControllerName", _culture);

        ViewBag.FirstName = UsersResources.ResourceManager.GetString("FirstName", _culture);
        ViewBag.LastName = UsersResources.ResourceManager.GetString("LastName", _culture);
        ViewBag.IsActive = BaseResources.ResourceManager.GetString("IsActive", _culture);
        ViewBag.MethodName = BaseResources.ResourceManager.GetString("MethodName", _culture);
        ViewBag.Name = BaseResources.ResourceManager.GetString("Name", _culture);
        ViewBag.SaveChanges = BaseResources.ResourceManager.GetString("SaveChanges", _culture);

        //Menus
        ViewBag.Roles = RolesResources.ResourceManager.GetString("Roles", _culture);
        ViewBag.Users = UsersResources.ResourceManager.GetString("Users", _culture);
        ViewBag.Home = BaseResources.ResourceManager.GetString("Home", _culture);
        ViewBag.Privacy = BaseResources.ResourceManager.GetString("Privacy", _culture);
    }
    // متد برای مقداردهی مقادیر ریسورس‌های Rolse
    protected void SetLocalizedValuesRoles()
    {
        ViewBag.ManageActionLogs = RolesResources.ResourceManager.GetString("ManageActionLogs", _culture);
        ViewBag.ManageRoles = RolesResources.ResourceManager.GetString("ManageRoles", _culture);
        ViewBag.BacktoRoles = RolesResources.ResourceManager.GetString("BacktoRoles", _culture);
        ViewBag.ManageActionLogsFor = RolesResources.ResourceManager.GetString("ManageActionLogsFor", _culture);
        ViewBag.ManageRolesFor = RolesResources.ResourceManager.GetString("ManageRolesFor", _culture);
        ViewBag.NoRoles = RolesResources.ResourceManager.GetString("NoRoles", _culture);
        ViewBag.RoleName = RolesResources.ResourceManager.GetString("RoleName", _culture);
        ViewBag.Roles = RolesResources.ResourceManager.GetString("Roles", _culture);
        ViewBag.Role = RolesResources.ResourceManager.GetString("Role", _culture);
        ViewBag.Actions = RolesResources.ResourceManager.GetString("Actions", _culture);
        ViewBag.RoleDetails = RolesResources.ResourceManager.GetString("RoleDetails", _culture);
    }
    // متد برای مقداردهی مقادیر ریسورس‌های User
    protected void SetLocalizedValuesUsers()
    {
        ViewBag.Users = UsersResources.ResourceManager.GetString("Users", _culture);
        ViewBag.CreateUser = UsersResources.ResourceManager.GetString("CreateUser", _culture);
        ViewBag.EnterFirstName = UsersResources.ResourceManager.GetString("EnterFirstName", _culture);
        ViewBag.EnterLastName = UsersResources.ResourceManager.GetString("EnterLastName", _culture);
        ViewBag.EnterEmailAddress = UsersResources.ResourceManager.GetString("EnterEmailAddress", _culture);
        ViewBag.EnterPassword = UsersResources.ResourceManager.GetString("EnterPassword", _culture);
        ViewBag.UserDetails = UsersResources.ResourceManager.GetString("UserDetails", _culture);
        ViewBag.BacktoUsers = UsersResources.ResourceManager.GetString("BacktoUsers", _culture);
        ViewBag.AlreadyHaveAnAccountLoginHere = UsersResources.ResourceManager.GetString("AlreadyHaveAnAccountLoginHere", _culture);
        ViewBag.CreateANewAccount = UsersResources.ResourceManager.GetString("CreateANewAccount", _culture);

    }
}
