using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using MHR_V02.Models.Base;

namespace MHR_V02.Filters
{
    public class LogActionFilter : IActionFilter
    {
        private readonly Data.ApplicationDbContext _context;

        public LogActionFilter(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var actionName = context.ActionDescriptor.RouteValues["action"];
            var controllerName = context.ActionDescriptor.RouteValues["controller"];

            var actionLog = new ActionLog
            {
                ControllerName = controllerName,
                ActionName = actionName,
                Timestamp = DateTime.UtcNow
                
            };

            _context.ActionLogs.Add(actionLog);
            _context.SaveChanges();

            Debug.WriteLine($"Controller: {controllerName}, Action: {actionName}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
