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
        public Candidate Candidate { get; set; }
        public Audit Audit { get; set; }

        //lookup values
        public IEnumerable<AuditDefinition> AuditDefinitions { get; set; }
        public IEnumerable<Quarter> Quarters { get; set; }
        public IEnumerable<AuditStatus> AuditStates { get; set; }
        public IEnumerable<PeriodType> PeriodTypes { get; set; }
    }
}