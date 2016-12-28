using SmartAudit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartAudit.ViewModel
{
    public class AuditSectionViewModel
    {
        public int AuditId { get; set; }
        public string AuditName { get; set; }
        public SectionDefinition Section { get; set; }
    }
}