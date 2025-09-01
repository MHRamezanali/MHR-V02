using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MHR_V02.Services;
using Microsoft.EntityFrameworkCore;
using MHR_V02.Data;

namespace MHR_V02.Filters
{
    public class AccessControlFilter : IAsyncActionFilter
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly ApplicationDbContext _context;

        public AccessControlFilter(IAuthorizationService authorizationService, ApplicationDbContext context)
        {
            _authorizationService = authorizationService;
            _context = context; // تزریق ApplicationDbContext
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userEmail = context.HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))//چک کردن خالی نبودن ایمیل
            {
                context.Result = new RedirectToActionResult("Login", "Users", null);
                return;
            }

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == userEmail);
            if (user == null)//چک کردن وجود داشتن کاربر
            {
                context.Result = new RedirectToActionResult("Login", "Users", null);
                return;
            }

            //bool isAdmin = user.UserRoles.Any(ur => ur.Role.Name == "Admin");
            //if (isAdmin)
            //{
            //    await next();
            //    return;
            //}

            var controller = context.RouteData.Values["controller"].ToString();
            var action = context.RouteData.Values["action"].ToString();
            var httpMethod = context.HttpContext.Request.Method;

            if (!await _authorizationService.HasAccess(user.Id, controller, action, httpMethod))
            {
                context.Result = new ForbidResult();
                return;
            }

            await next();
        }
    }
}
