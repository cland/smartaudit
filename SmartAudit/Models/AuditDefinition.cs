using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartAudit.Models
{
    public class AuditDefinition
    {
        public int Id { get; set; }
        [Required]
        [StringLength(150, ErrorMessage ="Name cannot be longer that 150 characters.")]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<SectionDefinition> Sections { get; set; }
    }
}