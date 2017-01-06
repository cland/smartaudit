using SmartAudit.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartAudit.Dtos
{
    public class AuditSimpleDto
    {
        public AuditSimpleDto()
        {
            SectionResults = new List<SectionResultsDto>();
        }
        public string Info { get; set; }
        public int Id { get; set; }
        public AuditDefinitionSimpleDto AuditDefinition { get; set; }
        public int AuditDefinitionId { get; set; }
        public int CandidateId { get; set; }
        public CandidateDto Candidate { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateInspectionCompleted { get; set; }
        public DateTime DateOfInspection { get; set; }

        public PeriodType PeriodType { get; set; }
        [Required]

        public int PeriodTypeId { get; set; }

        [Required]
        public int Year { get; set; }

        public int? Month { get; set; }

        public Quarter Quarter { get; set; }

        public int? QuarterId { get; set; }

        public AuditStatus AuditStatus { get; set; }
        [Required]

        public int AuditStatusId { get; set; }

        public double ScoreA
        {
            get
            {
                return (SectionResults != null ? SectionResults.Sum(sec => sec.ScoreA) : 0.0);
            }
        }
        public double ScoreB
        {
            get
            {                
                return (SectionResults != null ? SectionResults.Sum(sec => sec.ScoreB):0.0);
            }
        }
        
        public List<SectionResultsDto> SectionResults { get; set; }
    } //end class
}//end class