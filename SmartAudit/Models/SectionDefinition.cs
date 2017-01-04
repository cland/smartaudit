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
                return Questions.Sum(q => q.Weight);
            }
        }
        public double TotalSampleSize
        {
            get
            {
                return Questions.Sum(q => q.SampleSize);
            }
        }
        public int CountZeroTolerance
        {
            get
            {
                return Questions.Count(q => q.IsZeroTolerance);
            }
        }
        public int CountBonus
        {
            get
            {
                return Questions.Count(q => q.IsBonus);
            }
        }
        public virtual ICollection<QuestionDefinition> Questions { get; set; }
    }
}