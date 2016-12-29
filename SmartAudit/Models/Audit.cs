using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartAudit.Models
{
    public class Audit
    {
        public int Id { get; set; }
        public int AuditDefinitionId { get; set; }
        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateInspectionCompleted { get; set; }
        public DateTime DateOfInspection { get; set; }

        public PeriodType PeriodType { get; set; }
        [Required]
        [Display(Name ="Period Type")]
        public int PeriodTypeId { get; set; }

        [Required]
        public int Year { get; set; }

        public int? Month { get; set; }

        public Quarter Quarter { get; set; }
        [Display(Name ="Quarter")]
        public int? QuarterId { get; set; }

        public AuditStatus AuditStatus { get; set; }
        [Required]
        [Display(Name ="Status")]
        public int AuditStatusId { get; set; }

        public virtual ICollection<QuestionResult> Questions { get; set; }

    }
} //end class