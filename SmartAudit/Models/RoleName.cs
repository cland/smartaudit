using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartAudit.Models
{
    public class RoleName
    {
        public const string CanManageDefinitions = "CanManageDefinitions"; //highest
        public const string CanCreateUpdateSections = "CanCreateUpdateSections";
        public const string CanCreateUpdateQuestions = "CanCreateUpdateQuestions";
        public const string CanCreateUpdateAudit = "CanCreateUpdateAudit";      
    }
}