using System;
using System.Linq;
using MHR_V02.Data;
using MHR_V02.Models.Base; // Import your models here
using MHR_V02.Models.BasicTables;
using System.Security.Cryptography;


namespace MHR_V02.Data
{
    public class SeedDataService
    {
        private readonly ApplicationDbContext _context;

        public SeedDataService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Initialize(IServiceProvider serviceProvider)
        {
            // Check if the database already contains data
            if (_context.Users.Any())
            {
                return; // Database has been seeded
            }

            // Seed initial data
            var roles = new Role[]
            {
                new Role{ Name="SuperAdmin",Description="SuperAdmin",IsActive=true},
                new Role{ Name="Admin",Description="Admin",IsActive=true},
             };
            var users = new User[]
            {
                new User { FirstName = "Mahnaz", LastName = "Ramezanali", Email = "m.ramezanali@gmail.com",Password= HashPassword("1352"), IsActive = true },
                new User { FirstName = "Lashkari", LastName = "Raisani", Email = "mirlashkari@gmail.com",Password=HashPassword("1961"), IsActive = true  },
                // Add more seed data as needed
            };

            _context.Roles.AddRange(roles);
            _context.Users.AddRange(users);
            _context.SaveChanges();

            if (_context.Roles.Any() && _context.Users.Any())
            {
                var userRoles = new UserRole[]
                {
                    new UserRole{UserId= users.Where(c=> c.FirstName == "Mahnaz" && c.LastName == "Ramezanali" && c.Email == "m.ramezanali@gmail.com" && c.Password==users[0].Password).Select(c=>c.Id).FirstOrDefault(),RoleId= roles.Where(c=> c.Name=="SuperAdmin").Select(c=>c.Id).FirstOrDefault()},
                };
                _context.UserRoles.AddRange(userRoles);
                _context.SaveChanges();

            }
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);

        }
    }
}
