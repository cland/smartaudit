using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartAudit.Models
{
    public class AuditStatus
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public int Rank { get; set; }
    }
}