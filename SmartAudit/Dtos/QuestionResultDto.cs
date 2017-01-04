using SmartAudit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartAudit.Dtos
{
    public class QuestionResultDto
    {
        public int Id { get; set; }
        public QuestionDefinitionSimpleDto QuestionDefinition { get; set; }
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
        public bool isCorrect
        {
            get
            {
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
            get
            {
                if (isCorrect) return QuestionResult.FullPoints;
                if (isPartialCorrect) return QuestionResult.PartialPoints;
                return QuestionResult.NoPoints;
            }
        }

        public virtual ICollection<ActionComment> Feedback { get; set; }
        
    }
}