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

        public double ScoreA
        {
            get
            {
                return (isCorrect ? QuestionDefinition.Weight : 0.0);
            }
        }
        public double ScoreB
        {
            get
            {
                return (QuestionDefinition.IsZeroTolerance & (QuestionDefinition.ToleranceLimit < (QuestionDefinition.SampleSize-SampleActual)) ?
                    ScoreA :
                    ((Convert.ToDouble(this.SampleActual) / Convert.ToDouble(QuestionDefinition.SampleSize)) * QuestionDefinition.Weight) 
                    );
                
            }
        }

        public double effectiveScoreB()
        {
            if (IsNotApplicable) return 0;
            return ScoreB;
        }
        public double effectiveWeight()
        {
            return (QuestionDefinition.IsBonus || IsNotApplicable ? 0 : QuestionDefinition.Weight);
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