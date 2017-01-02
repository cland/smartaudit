using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartAudit.Dtos
{
    public class AuditResultsDto
    {
        public int AuditDefinitionId { get; set; }   
        public int AuditId { get; set; }
        public List<SectionResults> sections { get; set; }
    } //end of class

    public class SectionResults
    {
        public int SectionId { get; set; }
        public List<QuestionResults> questions { get; set; }
    }
    public class QuestionResults {
        public int Id { get; set; }
        public bool IsNotApplicable { get; set; }
        public int QuestionDefinitionId { get; set; }
        public int SampleActual { get; set; }
        public string SampleDescription { get; set; }
    }
} //end of namespace