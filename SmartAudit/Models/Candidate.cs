using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartAudit.Models
{
    public class Candidate
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Audit> Audits { get; set; }
    }
}