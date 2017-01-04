using SmartAudit.Dtos;
using SmartAudit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartAudit.ViewModel
{
    public class ShowAuditViewModel
    {
        public AuditSimpleDto Audit { get; set; }
        public List<SectionResultsDto> SectionResults { get; set; }

    }
}