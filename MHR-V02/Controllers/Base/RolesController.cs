using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MHR_V02.Data;
using MHR_V02.Models.Base;
using MHR_V02.Models.BasicTables;
using MHR_V02.ViewModels.Base;
using MHR_V02.Utilities;
using MHR_V02.Filters;
using Microsoft.Extensions.Localization;
using MHR_V02.Resources; 
using System.Globalization;
using System.Resources;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace MHR_V02.Controllers.Base
{
    public class RolesController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public RolesController(ApplicationDbContext context)
        {
            _context = context;
        }
        #region GET: Roles/Index
        [LoggableAction]
        [HttpGet]
        [ServiceFilter(typeof(AccessControlFilter))]
        public async Task<IActionResult> Index()
        {
            SetCommonLocalizedValues();
            SetLocalizedValuesRoles();
            

            return View(await _context.Roles.ToListAsync()) ; // پیام خطا از ریسورس




        }
        #endregion

        #region GET: Roles/Details/5
        [LoggableAction]
        [HttpGet]
        [ServiceFilter(typeof(AccessControlFilter))]
        public async Task<IActionResult> Details(Guid? id)
        {
            SetCommonLocalizedValues();
            SetLocalizedValuesRoles();
            if (id == null || _context.Roles == null)
            {
                return NotFound();
            }

            var role = await _context.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // GET: Roles/Create
        [LoggableAction]
        [HttpGet]
        [ServiceFilter(typeof(AccessControlFilter))]
        public IActionResult Create()
        {
            SetCommonLocalizedValues();
            SetLocalizedValuesRoles();
            return View();
        }
        #endregion

        #region Post: Roles/Create
        [LoggableAction]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(AccessControlFilter))]
        public async Task<IActionResult> Create(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var role = new Role
                {
                    Id = Guid.NewGuid(),
                    Name = roleViewModel.Name,
                    Description = roleViewModel.Description,
                    IsActive = roleViewModel.IsActive,
                    UserRoles = new List<UserRole>(),
                    RoleActions = new List<RoleAction>()
                };
                _context.Add(role);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Add log or debugging information here
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    // Log error messages
                }
            }
            return View(roleViewModel);
        }
        #endregion

        #region GET: Roles/Edit/5
        [LoggableAction]
        [HttpGet]
        [ServiceFilter(typeof(AccessControlFilter))]
        public async Task<IActionResult> Edit(Guid? id)
        {
            SetCommonLocalizedValues();
            SetLocalizedValuesRoles();
            if (id == null || _context.Roles == null)
            {
                return NotFound();
            }

            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }
        #endregion

        #region POST: Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [LoggableAction]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(AccessControlFilter))]
        public async Task<IActionResult> Edit(Guid id, RoleViewModel roleViewModel)
        {
            if (id != roleViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _context.Roles.FindAsync(id);
                    if (role == null)
                    {
                        return NotFound();
                    }

                    role.Name = roleViewModel.Name;
                    role.Description = roleViewModel.Description;
                    role.IsActive = roleViewModel.IsActive;
                    role.UserRoles = new List<UserRole>();
                    role.RoleActions = new List<RoleAction>();

                    _context.Update(role);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleExists(roleViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(roleViewModel);
        }
        #endregion

        #region GET: Roles/Delete/5
        [LoggableAction]
        [HttpGet]
        [ServiceFilter(typeof(AccessControlFilter))]
        public async Task<IActionResult> Delete(Guid? id)
        {
            SetCommonLocalizedValues();
            SetLocalizedValuesRoles();
            if (id == null || _context.Roles == null)
            {
                return NotFound();
            }

            var role = await _context.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }
        #endregion

        #region POST: Roles/Delete/5
        [LoggableAction]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(AccessControlFilter))]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Roles == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Roles'  is null.");
            }
            var role = await _context.Roles.FindAsync(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region GET: Roles/ManageActionLogs
        [LoggableAction]
        [HttpGet]
        [ServiceFilter(typeof(AccessControlFilter))]
        public async Task<IActionResult> ManageActionLogs(Guid id, int pageNumber = 1, int pageSize = 10)
        {
            SetCommonLocalizedValues();
            SetLocalizedValuesRoles();

            var role = await _context.Roles
                .Include(u => u.RoleActions)
                .ThenInclude(ur => ur.ActionLog)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (role == null)
            {
                return NotFound();
            }

            var allActionLogs = await _context.ActionLogs.ToListAsync();
            var roleActions = role.RoleActions.Select(ur => ur.ActionLogId).ToList();

            // صفحه‌بندی سمت سرور
            var pagedActionLogs = allActionLogs
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(action => new ActionLogViewModel
                {
                    Id = action.Id,
                    ControllerName = action.ControllerName,
                    ActionName = action.ActionName,
                    HttpMethod = action.HttpMethod,
                    IsSelected = roleActions.Contains(action.Id)
                })
                .OrderBy(c => c.ControllerName)
                .ThenBy(c => c.ActionName)
                .ThenBy(c => c.HttpMethod)
                .ToList();

            var model = new ManageActionLogsViewModel
            {
                RoleId = role.Id,
                RoleName = $"{role.Name}",
                ActionLogs = pagedActionLogs,
                TotalCount = allActionLogs.Count // تعداد کل رکوردها برای صفحه‌بندی
            };

            return View(model);
        }

        #endregion

        #region POST: Roles/ManageActionLogs
        [LoggableAction]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(AccessControlFilter))]
        public async Task<IActionResult> ManageActionLogs(ManageActionLogsViewModel model)
        {
            if (model == null || model.ActionLogs == null)
            {
                return BadRequest("Invalid data submitted.");
            }

            var role = await _context.Roles.Include(u => u.RoleActions)
                                           .FirstOrDefaultAsync(u => u.Id == model.RoleId);
            if (role == null)
            {
                return NotFound("Role not found.");
            }

            // حذف تمام اکشن های موجود نقش
            role.RoleActions.Clear();

            // افزودن اکشن های جدید
            foreach (var actionLog in model.ActionLogs.Where(r => r.IsSelected))
            {
                role.RoleActions.Add(new RoleAction { RoleId = model.RoleId, ActionLogId = actionLog.Id });
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        #endregion

        [HttpGet]
        private bool RoleExists(Guid id)
        {
          return (_context.Roles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
     
        


    }
}
