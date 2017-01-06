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
        public int Rank { get; set; }
        public bool IsActive { get; set; }

        //Navigational property
        public AuditDefinition AuditDefinition { get; set; }
        public int AuditDefinitionId { get; set; }
        public double TotalPossibleScore
        {
            get
            {
                return (Questions != null ? Questions.Sum(q => q.Weight):0.0);
            }
        }
        public double TotalSampleSize
        {
            get
            {
                return (Questions != null ? Questions.Sum(q => q.SampleSize):0.0);
            }
        }
        public int CountZeroTolerance
        {
            get
            {
                return (Questions != null ? Questions.Count(q => q.IsZeroTolerance):0);
            }
        }
        public int CountBonus
        {
            get
            {
                return (Questions != null ? Questions.Count(q => q.IsBonus):0);
            }
        }
        public virtual ICollection<QuestionDefinition> Questions { get; set; }
    }
}