using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartAudit.Dtos
{
    public class NewSectionDto
    {
        public int AuditDefinitionId { get; set; }
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        public double Weighting { get; set; }
        public string Status { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
    }//end class
} //end namespace