using SmartAudit.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartAudit.Dtos
{
    public class AuditDefinitionSimpleDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "Name cannot be longer that 200 characters.")]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }        
        
        public double TotalSectionWeighting
        {
            get
            {
                return Sections.Where(s => s.IsActive == true).Sum(s => s.Weighting);
            }
        }
        public virtual ICollection<SectionDefinitionSimpleDto> Sections { get; set; }
    }
}