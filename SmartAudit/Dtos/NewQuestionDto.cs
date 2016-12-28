using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartAudit.Dtos
{
    public class NewQuestionDto
    {
        public int SectionDefinitionId { get; set; }
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        public string DisplayNumber { get; set; }

        [Required]
        public string QuestionText { get; set; }

        [Required]
        public string Guideline { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public double Weight { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int SampleSize { get; set; }

        public bool IsZeroTolerance { get; set; }
        public int ToleranceLimit { get; set; }
        public bool IsBonus { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
    } //end class
} //end namespace