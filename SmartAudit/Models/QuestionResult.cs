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
        public bool IsApplicable { get; set; }
        public string SampleDescription { get; set; }

        public double Score { get
            {
                return (this.SampleActual / QuestionDefinition.SampleSize) * QuestionDefinition.Weight;
            }
        }
        public virtual ICollection<ActionComment> Feedback { get; set; }

    }
}