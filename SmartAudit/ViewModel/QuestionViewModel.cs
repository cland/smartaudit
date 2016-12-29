using SmartAudit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartAudit.ViewModel
{
    public class QuestionViewModel
    {        
        public string AuditName { get; set; }
        public SectionDefinition Section { get; set; }
        public QuestionDefinition Question { get; set; }
    }
}