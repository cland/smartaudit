using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartAudit.Models
{
    public class SectionDefinition
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        public double Weighting { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }

        //Navigational property
        public int AuditDefinitionId { get; set; }
        public virtual ICollection<QuestionDefinition> Questions { get; set; }
    }
}