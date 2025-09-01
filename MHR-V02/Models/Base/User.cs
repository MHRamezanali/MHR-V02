using MHR_V02.Models.Base;
using MHR_V02.Models.BasicTables;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace MHR_V02.Models.Base
{
    public class User : BaseEntity
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
            UserActions = new HashSet<UserAction>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<UserAction> UserActions { get; set; }       
    }
}
