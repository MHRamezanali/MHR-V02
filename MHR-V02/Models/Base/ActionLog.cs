using MHR_V02.Models.Base;
using MHR_V02.Models.BasicTables;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace MHR_V02.Models.Base
{
    public class ActionLog
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string ControllerName { get; set; }

        [Required]
        [StringLength(100)]
        public string ActionName { get; set; }

        [Required]
        [StringLength(10)]
        public string HttpMethod { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [Required]
        [StringLength(210)] // فرض کنیم حداکثر طول ControllerName + ActionName + HttpMethod کمتر از 210 کاراکتر باشد
        public string UniqueKey { get; set; }

        public void SetUniqueKey()
        {
            UniqueKey = $"{ControllerName}.{ActionName}.{HttpMethod}";
        }

        public ICollection<UserAction> UserActions { get; set; }
        public ICollection<RoleAction> RoleActions { get; set; }
    }
}
