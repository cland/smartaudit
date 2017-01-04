using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartAudit.Models
{
    public class Audit
    {
        public Audit()
        {
            Year = DateTime.Now.Year;
            DateOfInspection = DateTime.Now;
            DateCreated = DateTime.Now;
            Month = DateTime.Now.Month;          
        }
        public int Id { get; set; }
        public AuditDefinition AuditDefinition { get; set; }
        [Display(Name = "Audit Definition")]
        public int AuditDefinitionId { get; set; }

        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }

        public DateTime DateCreated { get; set; }
        [Display(Name = "Completion Date")]
        public DateTime? DateInspectionCompleted { get; set; }
        [Display(Name = "Inspection Date")]
        public DateTime DateOfInspection { get; set; }

        public PeriodType PeriodType { get; set; }
        [Required]
        [Display(Name ="Period Type")]
        public byte PeriodTypeId { get; set; }

        [Required]
        public int Year { get; set; }

        public int? Month { get; set; }

        public Quarter Quarter { get; set; }
        [Display(Name ="Quarter")]
        public byte? QuarterId { get; set; }

        public AuditStatus AuditStatus { get; set; }
        [Required]
        [Display(Name ="Status")]
        public byte AuditStatusId { get; set; }

        public virtual ICollection<QuestionResult> QuestionResults { get; set; }

    }
} //end class