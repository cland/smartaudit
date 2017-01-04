using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartAudit.Models
{
    public class QuestionResult
    {
        public int Id { get; set; }
        public QuestionDefinition QuestionDefinition { get; set; }
        public int QuestionDefinitionId { get; set; }  //The question will determine the section
        public int AuditId { get; set; }

        public int SampleActual { get; set; }
        public bool IsNotApplicable { get; set; }
        public string SampleDescription { get; set; }

        public double WeightedScore
        {
            get
            {
                return (System.Convert.ToDouble(this.SampleActual) / System.Convert.ToDouble(QuestionDefinition.SampleSize)) * QuestionDefinition.Weight;
            }
        }
        public double AbsoluteScore
        {
            get
            {
                return (isCorrect ? QuestionDefinition.Weight : 0);
            }
        }
        public bool isCorrect { get {
                return (SampleActual == QuestionDefinition.SampleSize);
            }
        }
        public bool isPartialCorrect
        {
            get
            {
                return (SampleActual > 0 & !isCorrect);
            }
        }
        public string scoreDescription
        {
            get {
                if (isCorrect) return FullPoints;
                if (isPartialCorrect) return PartialPoints;
                return NoPoints;
            }
        }

        public virtual ICollection<ActionComment> Feedback { get; set; }

        public static readonly string FullPoints = "smartaudit-fullpoints";
        public static readonly string PartialPoints = "smartaudit-partialpoints";
        public static readonly string NoPoints = "smartaudit-nopoints";

    }
}