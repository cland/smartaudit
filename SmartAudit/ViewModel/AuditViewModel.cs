using SmartAudit.Dtos;
using SmartAudit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartAudit.ViewModel
{
    public class AuditViewModel
    {
        public AuditDefinition AuditDefinition { get; set; }
        public AuditSimpleDto Audit { get; set; }
       // public IEnumerable<QuestionResult> QuestionResults { get; set; }
        public List<SectionResultsDto> SectionResults { get; set; }
        //lookup values
        public IEnumerable<AuditDefinition> AuditDefinitions { get; set; }
        public IEnumerable<Quarter> Quarters { get; set; }
        public IEnumerable<AuditStatus> AuditStates { get; set; }
        public IEnumerable<PeriodType> PeriodTypes { get; set; }
    }
}