using SmartAudit.Models;
using SmartAudit.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartAudit.Controllers
{
    /*
     * CRUDS for AUDIT-DEFINITION / SECTION-DEFINITION / QUESTION-DEFINITION 
     */
    public class DefinitionController : Controller
    {
        private ApplicationDbContext _context;
        private static string _AuditDefinitionForm = "AuditDefinitionForm";
        private static string _ShowAuditDefinition = "ShowAuditDefinition";
        private static string _ShowSection = "ShowSection";
        private static string _QuestionDefintionForm = "QuestionDefinitionForm";
       // private static string _SectionDefinitionForm = "SectionDefinitionForm";
       // private static string _QuestionDefinitionForm = "QuestionDefinitionForm";

        public DefinitionController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // AUDIT-DEFINITION CRUDS
        public ViewResult Index()
        {
            //front end to use api
            return View();
        }

        public ActionResult ShowAudit(int id)
        {
            var auditdefinition = _context.AuditDefinitions                
                .SingleOrDefault(a => a.Id == id);
            if (auditdefinition == null) return HttpNotFound();
            return View(_ShowAuditDefinition,auditdefinition);
        }

        [Authorize(Roles = RoleName.CanManageDefinitions)]
        public ActionResult NewAudit()
        {
            var viewModel = new AuditDefinitionFormViewModel
            {
                AuditDefinition = new AuditDefinition(),
                SectionCount = 0
            };
            return View(_AuditDefinitionForm,viewModel);
        } //

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageDefinitions)]
        public ActionResult SaveAudit(AuditDefinition auditDefinition)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new AuditDefinitionFormViewModel
                {
                    AuditDefinition = auditDefinition,
                    SectionCount = 0
                };
                return View(_AuditDefinitionForm, viewModel);
            }
            if (auditDefinition.Id == 0)
            {
                //new
                _context.AuditDefinitions.Add(auditDefinition);
            }
            else {
                //existing
                var auditDefinitionInDb = _context.AuditDefinitions.Single(a => a.Id == auditDefinition.Id);
                auditDefinitionInDb.Name = auditDefinition.Name;
                auditDefinitionInDb.Description = auditDefinition.Description;
                auditDefinitionInDb.IsActive = auditDefinition.IsActive;
            }

            _context.SaveChanges();
            return RedirectToAction("Index","Definition");
        } //

        [Authorize(Roles = RoleName.CanManageDefinitions)]
        public ActionResult EditAudit(int id)
        {
            var auditDefinition = _context.AuditDefinitions.SingleOrDefault(a => a.Id == id);
            if (auditDefinition == null) return HttpNotFound();

            var viewModel = new AuditDefinitionFormViewModel
            {
                AuditDefinition = auditDefinition,
                SectionCount = 0
            };
            return View(_AuditDefinitionForm, viewModel);            
        } //


        // SECTION-DEFINITION CRUDS
        public ViewResult NewSection(int auditId = 0)
        {
            var auditDefinition = _context.AuditDefinitions.SingleOrDefault(a => a.Id == auditId);
            if (auditDefinition == null) return View(new AuditDefinition());
            //front end to use api
            return View(auditDefinition);
        }

        public ActionResult ShowSection(int id)
        {
            var sectionDefinition = _context.SectionDefinitions
                .SingleOrDefault(a => a.Id == id);
            if (sectionDefinition == null) return HttpNotFound("Sectin definition with id '" + id + "' not found");



            var auditDefinition = _context.AuditDefinitions.SingleOrDefault(a=>a.Id == sectionDefinition.AuditDefinitionId);            
            if (auditDefinition == null) return HttpNotFound("Audit definition not found!");
            
            var viewModel = new AuditSectionViewModel
            {
                AuditId = auditDefinition.Id,
                AuditName = auditDefinition.Name,
                Section = sectionDefinition
            };
            return View(_ShowSection, viewModel);
        }

        // QUESTION-DEFINITION CRUDS

        public ViewResult NewQuestion(int sectionId = 0)
        {
            var sectionDefinition = _context.SectionDefinitions.SingleOrDefault(a => a.Id == sectionId);
            if (sectionDefinition == null) return View(new AuditDefinition());

            var auditDefinition = _context.AuditDefinitions.SingleOrDefault(a => a.Id == sectionDefinition.AuditDefinitionId);
            var viewModel = new AuditSectionViewModel
            {
                AuditId = auditDefinition.Id,
                AuditName = auditDefinition.Name,
                Section = sectionDefinition
            };
            
            return View(_QuestionDefintionForm, viewModel);
        }
    } // end class
} //end namespace