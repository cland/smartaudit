using SmartAudit.Models;
using SmartAudit.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartAudit.Controllers
{
    public class AuditController : Controller
    {
        private ApplicationDbContext _context;
        private static string _AuditForm = "AuditForm";
        private static string _ShowAudit = "ShowAudit";
        

        public AuditController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Audit
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = RoleName.CanManageDefinitions)]
        public ActionResult NewAudit(int Id = 0)
        {
            var auditDefinition = new AuditDefinition();

            if(Id > 0)
                auditDefinition =_context.AuditDefinitions.SingleOrDefault(a => a.Id == Id);

            var viewModel = new AuditViewModel
            {
                AuditDefinition = auditDefinition,
                Candidate = new Candidate(),
                Audit = new Audit(),
                AuditDefinitions = _context.AuditDefinitions.ToList(),
                PeriodTypes = _context.PeriodTypes.ToList(),
                AuditStates = _context.AuditStates.ToList(),
                Quarters = _context.Quarters.ToList()
            };
            return View(_AuditForm, viewModel);
        } //

        public ActionResult ShowAudit(int id)
        {
            var audit = _context.Audits
                .SingleOrDefault(a => a.Id == id);
            if (audit == null) return HttpNotFound();
            return View(_ShowAudit, audit);
        }

        
    } //end class
} //end namespace