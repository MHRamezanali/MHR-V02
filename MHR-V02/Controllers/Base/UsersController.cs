using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MHR_V02.Data;
using MHR_V02.Models.Base;
using MHR_V02.Utilities;
using MHR_V02.ViewModels.Base;
using MHR_V02.Models.BasicTables;
using System.Text;
using System.Security.Cryptography;
using MHR_V02.Filters;
using Microsoft.CodeAnalysis.Scripting;
using System.Linq.Expressions;

namespace MHR_V02.Controllers.Base
{
    public class UsersController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public UsersController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        #region GET: Users/Index
        [LoggableAction]
        [HttpGet]
        [ServiceFilter(typeof(AccessControlFilter))]
        public async Task<IActionResult> Index()
        {
            SetCommonLocalizedValues();
            SetLocalizedValuesRoles();
            if (_context.Users == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Users' is null.");
            }

            var users = await _context.Users
        .Include(u => u.UserRoles)
        .ThenInclude(ur => ur.Role)
        .Select(u => new UserViewModel
        {
            Id = u.Id,
            FirstName = u.FirstName,
            LastName = u.LastName,
            Email = u.Email,
            IsActive = u.IsActive,
            Roles = u.UserRoles.Select(ur => ur.Role.Name).ToList()
        })
        .ToListAsync();

            return View(users);
        }
        #endregion

        #region GET: Users/Details/5
        [LoggableAction]
        [HttpGet]
        [ServiceFilter(typeof(AccessControlFilter))]
        public async Task<IActionResult> Details(Guid? id)
        {
            SetCommonLocalizedValues();
            SetLocalizedValuesUsers();
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            // بازیابی کاربر و تبدیل به ViewModel
            var userViewModel = await _context.Users
                .Where(u => u.Id == id)
                .Select(user => new UserViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    IsActive = user.IsActive
                })
                .FirstOrDefaultAsync();

            if (userViewModel == null)
            {
                return NotFound();
            }

            return View(userViewModel);
        }
        #endregion

        #region GET: Users/Create
        [LoggableAction]
        [HttpGet]
        [ServiceFilter(typeof(AccessControlFilter))]
        public IActionResult Create()
        {
            SetCommonLocalizedValues();
            SetLocalizedValuesUsers();
            return View();
        }
        #endregion

        #region POST: Users/Create
        [LoggableAction]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(AccessControlFilter))]
        public async Task<IActionResult> Create(UserCreateViewModel createUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = createUserViewModel.FirstName,
                    LastName = createUserViewModel.LastName,
                    Email = createUserViewModel.Email,
                    Password = HashPassword(createUserViewModel.Password),
                    IsActive = createUserViewModel.IsActive,
                    UserRoles = new List<UserRole>(),
                    UserActions = new List<UserAction>()
                };

                _context.Add(user);
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
            return View(createUserViewModel);
        }
        #endregion

        #region GET: Users/Edit/5
        [LoggableAction]
        [HttpGet]
        [ServiceFilter(typeof(AccessControlFilter))]
        public async Task<IActionResult> Edit(Guid? id)
        {
            SetCommonLocalizedValues();
            SetLocalizedValuesUsers();
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }
            // بازیابی کاربر و تبدیل به ViewModel
            var userViewModel = await _context.Users
                .Where(u => u.Id == id)
                .Select(user => new UserViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    IsActive = user.IsActive
                })
                .FirstOrDefaultAsync();

            if (userViewModel == null)
            {
                return NotFound();
            }

            return View(userViewModel);
        }
        #endregion

        #region POST: Users/Edit/5
        [LoggableAction]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(AccessControlFilter))]
        public async Task<IActionResult> Edit(Guid id, UserCreateViewModel userViewModel)
        {
            if (id != userViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _context.Users.FindAsync(id);
                    if (user == null)
                    {
                        return NotFound();
                    }

                    user.FirstName = userViewModel.FirstName;
                    user.LastName = userViewModel.LastName;
                    user.Email = userViewModel.Email;
                    user.IsActive = userViewModel.IsActive;
                    //user.UserRoles = new List<UserRole>();
                    //user.UserActions = new List<UserAction>();
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(userViewModel.Id))
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
            return View(userViewModel);
        }
        #endregion

        #region GET: Users/Delete/5
        [LoggableAction]
        [HttpGet]
        [ServiceFilter(typeof(AccessControlFilter))]
        public async Task<IActionResult> Delete(Guid? id)
        {
            SetCommonLocalizedValues();
            SetLocalizedValuesUsers();
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            // بازیابی کاربر و تبدیل به ViewModel
            var userViewModel = await _context.Users
                .Where(u => u.Id == id)
                .Select(user => new UserViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    IsActive = user.IsActive
                })
                .FirstOrDefaultAsync();

            if (userViewModel == null)
            {
                return NotFound();
            }

            return View(userViewModel);
        }
        #endregion

        #region POST: Users/Delete/5
        [LoggableAction]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(AccessControlFilter))]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region GET: Users/Login
        [LoggableAction]
        [HttpGet]
        public IActionResult Login()
        {
            SetCommonLocalizedValues();
            SetLocalizedValuesUsers();
            return View();
        }
        #endregion

        #region POST: Users/Login
        [LoggableAction]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var masterPassword = HashPassword(_configuration["MasterPassword"]);

            if (ModelState.IsValid)
            {
                // پیدا کردن کاربر با ایمیل
                var user = _context.Users.FirstOrDefault(u => u.Email == model.Email);

                if (user != null)
                {
                    // بررسی مستر پسورد
                    if (VerifyPassword(model.Password, masterPassword) ||
                        VerifyPassword(model.Password, user.Password))
                    {
                        // Login success, set session or cookie
                        HttpContext.Session.SetString("UserEmail", user.Email);
                        HttpContext.Session.SetString("UserId", user.Id.ToString());
                        return RedirectToAction("Index", "Home");
                    }
                }

                // اگر ایمیل یا رمز عبور اشتباه باشد
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }
        #endregion

        #region POST: Users/Logout
        [LoggableAction]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            // Clear the session or cookie
            HttpContext.Session.Remove("UserEmail");
            HttpContext.Session.Remove("UserId");
            return RedirectToAction("Login", "Users");
        }
        #endregion

        #region GET: Users/Register
        [LoggableAction]
        [HttpGet]
        public IActionResult Register()
        {
            SetCommonLocalizedValues();
            SetLocalizedValuesUsers();
            return View();
        }
        #endregion

        #region POST: Users/Register
        [LoggableAction]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if the email already exists
                if (_context.Users.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use.");
                    return View(model);
                }

                // Create a new user
                var user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = HashPassword(model.Password), // Hash the password
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
        #endregion

        #region GET: Users/ManageRoles
        [LoggableAction]
        [HttpGet]
        [ServiceFilter(typeof(AccessControlFilter))]    
        public async Task<IActionResult> ManageRoles(Guid id)
        {
            SetCommonLocalizedValues();
            SetLocalizedValuesRoles();
            SetLocalizedValuesUsers();
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var allRoles = await _context.Roles.ToListAsync();
            var userRoles = user.UserRoles.Select(ur => ur.RoleId).ToList();

            var model = new ManageRolesViewModel
            {
                UserId = user.Id,
                UserName = $"{user.FirstName} {user.LastName}",
                Roles = allRoles.Select(role => new RoleViewModel
                {
                    Id = role.Id,
                    Name = role.Name,
                    IsSelected = userRoles.Contains(role.Id)
                }).ToList() // مقداردهی به لیست Roles
            };

            return View(model);
        }
        #endregion

        #region POST: Users/ManageRoles
        [LoggableAction]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(AccessControlFilter))]
        public async Task<IActionResult> ManageRoles(ManageRolesViewModel model)
        {
            if (model == null || model.Roles == null)
            {
                return BadRequest("Invalid data submitted.");
            }

            var user = await _context.Users.Include(u => u.UserRoles)
                                           .FirstOrDefaultAsync(u => u.Id == model.UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // حذف تمام نقش‌های موجود کاربر
            user.UserRoles.Clear();

            // افزودن نقش‌های جدید
            foreach (var role in model.Roles.Where(r => r.IsSelected))
            {
                user.UserRoles.Add(new UserRole { UserId = model.UserId, RoleId = role.Id });
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region GET: Users/ManageActionLogs
        [LoggableAction]
        [HttpGet]
        [ServiceFilter(typeof(AccessControlFilter))]
        public async Task<IActionResult> ManageActionLogs(Guid id)
        {
            SetCommonLocalizedValues();
            SetLocalizedValuesRoles();
            SetLocalizedValuesUsers();
            var user = await _context.Users
                .Include(u => u.UserActions)
                .ThenInclude(ur => ur.ActionLog)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var allActionLogs = await _context.ActionLogs.ToListAsync();
            var userActions = user.UserActions.Select(ur => ur.ActionLogId).ToList();

            var model = new ManageActionLogsViewModel
            {
                UserId = user.Id,
                UserName = $"{user.FirstName} {user.LastName}",
                ActionLogs = allActionLogs.Select(action => new ActionLogViewModel
                {
                    Id = action.Id,
                    ControllerName = action.ControllerName,
                    ActionName = action.ActionName,
                    HttpMethod = action.HttpMethod,
                    IsSelected = userActions.Contains(action.Id)
                }).OrderBy(c => c.ControllerName).ThenBy(c => c.ActionName).ThenBy(c => c.HttpMethod).ToList() // مقداردهی به لیست ActionLogs
            };

            return View(model);
        }
        #endregion

        #region POST: Users/ManageActions
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

            var user = await _context.Users.Include(u => u.UserActions)
                                           .FirstOrDefaultAsync(u => u.Id == model.UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // حذف تمام اکشن های موجود نقش
            user.UserActions.Clear();

            // افزودن اکشن های جدید
            foreach (var actionLog in model.ActionLogs.Where(r => r.IsSelected))
            {
                user.UserActions.Add(new UserAction { UserId = model.UserId, ActionLogId = actionLog.Id });
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Helper method to hash passwords
        [LoggableAction]
        [HttpGet]
        [ServiceFilter(typeof(AccessControlFilter))]
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);

        }
        #endregion
        #region GET:VerifyPassword
        [LoggableAction]
        [HttpGet]
        [ServiceFilter(typeof(AccessControlFilter))]
        public bool VerifyPassword(string plainPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
        }

        #endregion

        private bool UserExists(Guid id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        #region ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(AccessControlFilter))]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            SetCommonLocalizedValues();
            SetLocalizedValuesUsers();
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _context.Users.FindAsync(model.UserId);
            if (user == null)
            {
                return NotFound();
            }

            // اعتبارسنجی رمز فعلی
            var currentHashedPassword = HashPassword(model.CurrentPassword);
            if (user.Password != currentHashedPassword)
            {
                ModelState.AddModelError(string.Empty, "رمز عبور فعلی اشتباه است.");
                return View(model);
            }

            // به‌روزرسانی رمز عبور
            user.Password = HashPassword(model.NewPassword);
            _context.Update(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Users");
        }

        #endregion

    }
}
