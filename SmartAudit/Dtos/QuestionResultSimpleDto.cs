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
                return (this.SampleActual / QuestionDefinition.SampleSize) * QuestionDefinition.Weight;
            }
        }
        
    }
}