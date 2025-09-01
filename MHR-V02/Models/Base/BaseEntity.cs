using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MHR_V02.Models.Base
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public bool IsDeleted { get; set; } = false;


        //Audit Fields
        public Guid? CreatedById { get; set; } 
        public Guid? UpdatedById { get; set; }
        public Guid? DeletedById { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }      
    }
}
