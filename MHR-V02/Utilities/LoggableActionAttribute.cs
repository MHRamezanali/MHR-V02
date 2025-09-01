using System;

namespace MHR_V02.Utilities
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class LoggableActionAttribute : Attribute
    {
    }
}
