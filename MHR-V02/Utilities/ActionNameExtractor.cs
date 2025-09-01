using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MHR_V02.Data;
using MHR_V02.Models.Base;
using MHR_V02.Utilities;

namespace MHR_V02.Utilities
{
    public class ActionNameExtractor
    {
        public static void ExtractActionNames(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<Data.ApplicationDbContext>();

            var controllers = Assembly.GetExecutingAssembly()
                                      .GetTypes()
                                      .Where(type => typeof(Controller).IsAssignableFrom(type));

            foreach (var controller in controllers)
            {
                var actions = controller.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                        .Where(method => method.GetCustomAttribute<LoggableActionAttribute>() != null);

                foreach (var action in actions)
                {
                    var controllerName = controller.Name;
                    var actionName = action.Name;

                    // Get the HTTP method (POST or GET)
                    var httpMethod = "UNKNOWN";
                    if (action.GetCustomAttribute<HttpPostAttribute>() != null)
                    {
                        httpMethod = "POST";
                    }
                    else if (action.GetCustomAttribute<HttpGetAttribute>() != null)
                    {
                        httpMethod = "GET";
                    }

                    // Check if the action is already logged
                    var actionLog = new ActionLog
                    {
                        Id = new Guid(),
                        ControllerName = controllerName,
                        ActionName = actionName,
                        Timestamp = DateTime.Now,
                        HttpMethod = httpMethod
                    };

                    actionLog.SetUniqueKey(); // Set the unique key

                    if (!context.ActionLogs.Any(al => al.UniqueKey == actionLog.UniqueKey))

                    {
                        context.ActionLogs.Add(actionLog);
                    }
                }
            }

            context.SaveChanges();
        }
    }
}
