using SmartAudit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartAudit.Dtos
{
    public class QuestionResultSimpleDto
    {
        public int Id { get; set; }
        public QuestionDefinition QuestionDefinition { get; set; }
        public int AuditId { get; set; }

        public int SampleActual { get; set; }
        public bool IsNotApplicable { get; set; }
        public string SampleDescription { get; set; }

        public double Score
        {
            get
            {
                return (System.Convert.ToDouble(this.SampleActual) / System.Convert.ToDouble(QuestionDefinition.SampleSize)) * QuestionDefinition.Weight;
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

    }
}