using MHR_V02.Data;
using Microsoft.EntityFrameworkCore;

namespace MHR_V02.Services
{
    public interface IAuthorizationService
    {
        Task<bool> HasAccess(Guid userId, string controller, string action, string httpMethod);
    }

    public class AuthorizationService : IAuthorizationService
    {
        private readonly ApplicationDbContext _context;

        public AuthorizationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> HasAccess(Guid userId, string controller, string action, string httpMethod)
        {
            var userRoles = await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.RoleId)
                .ToListAsync();

            var hasUserAccess = await _context.UserActions
                .AnyAsync(ua => ua.UserId == userId && ua.ActionLog.ControllerName == controller + "Controller" && ua.ActionLog.ActionName == action && ua.ActionLog.HttpMethod == httpMethod);

            var hasAdminRole = await _context.Roles
                .AnyAsync(r => userRoles.Contains(r.Id) && r.Name == "SuperAdmin");
            if (hasAdminRole)
            {
                return true;
            }
            var hasRoleAccess = await _context.RoleActions
                .AnyAsync(ra => userRoles.Contains(ra.RoleId) && ra.ActionLog.ControllerName == controller+ "Controller" && ra.ActionLog.ActionName == action && ra.ActionLog.HttpMethod == httpMethod);

            return hasUserAccess || hasRoleAccess;
        }
    }
}
