using OfficeOpenXml;
using SmartAudit.Models;
using SmartAudit.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
        private static string _QuestionDefinitionForm = "QuestionDefinitionForm";
        private static string _SectionDefinitionForm = "SectionDefinitionForm";
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

        public ActionResult CopyAudit(int id)
        {
            //TODO: Copy audit to be implemented
            var auditdefinition = _context.AuditDefinitions
                .SingleOrDefault(a => a.Id == id);
            if (auditdefinition == null) return HttpNotFound();
            return View(_ShowAuditDefinition, auditdefinition);
        }

        // SECTION-DEFINITION CRUDS
        public ViewResult NewSection(int auditId = 0)
        {
            var auditDefinition = _context.AuditDefinitions.SingleOrDefault(a => a.Id == auditId);
            if (auditDefinition == null) return View(new AuditDefinition());

            var viewModel = new AuditSectionViewModel
            {
                AuditId = auditDefinition.Id,
                AuditName = auditDefinition.Name,
                Section = new SectionDefinition()
            };
            
            return View(_SectionDefinitionForm,viewModel);
        }
        [Authorize(Roles = RoleName.CanManageDefinitions)]
        public ActionResult EditSection(int id)
        {
            var sectionDefinition = _context.SectionDefinitions.SingleOrDefault(a => a.Id == id);
            if (sectionDefinition == null) return HttpNotFound("Invalid question id '" + id + "'");

            var auditDefinition = _context.AuditDefinitions.SingleOrDefault(s => s.Id == sectionDefinition.AuditDefinitionId);
            var viewModel = new AuditSectionViewModel
            {
                AuditId = auditDefinition.Id,
                AuditName = auditDefinition.Name,
                Section = sectionDefinition
            };
            return View(_SectionDefinitionForm, viewModel);
        } //
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
            var viewModel = new QuestionViewModel
            {
                AuditName = auditDefinition.Name,
                Section = sectionDefinition,
                Question = new QuestionDefinition()
            };

            return View(_QuestionDefinitionForm, viewModel);
        } //

        [Authorize(Roles = RoleName.CanManageDefinitions)]
        public ActionResult EditQuestion(int id)
        {
            var questionDefinition = _context.QuestionDefinitions.SingleOrDefault(a => a.Id == id);
            if (questionDefinition == null) return HttpNotFound("Invalid question id '" + id + "'");

            var sectionDefinition = _context.SectionDefinitions.SingleOrDefault(s => s.Id == questionDefinition.SectionDefinitionId);
            var auditDefinition = _context.AuditDefinitions.SingleOrDefault(s => s.Id == sectionDefinition.AuditDefinitionId);
            var viewModel = new QuestionViewModel
            {                
                AuditName = auditDefinition.Name,
                Section = sectionDefinition,
                Question = questionDefinition
            };
            return View(_QuestionDefinitionForm, viewModel);
        } //

        [Authorize(Roles = RoleName.CanManageDefinitions)]
        public ViewResult UploadAuditDefinition()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = RoleName.CanManageDefinitions)]        
        public ActionResult UploadExcelFile(HttpPostedFileBase FileUpload)
        {
            var audit_count = 0;
            AuditDefinition auditDefinition = null;
            using (var excel = new ExcelPackage(FileUpload.InputStream))
            {
                var auditDefinitionDt = excel.ToDataTable("Audits");
                var sectionsDt = excel.ToDataTable("Sections");
                var questionsDt = excel.ToDataTable("Questions");
                
                foreach (DataRow auditRow in auditDefinitionDt.Rows)
                {
                    auditDefinition = new AuditDefinition();
                    auditDefinition.Name = auditRow["Name"].ToString();
                    auditDefinition.Description = auditRow["Description"].ToString();
                    auditDefinition.IsActive = Convert.ToBoolean((string)auditRow["IsActive"]);
                    _context.AuditDefinitions.Add(auditDefinition);
                    audit_count++;
                    //go through the sections
                    DataRow[] foundRows = sectionsDt.Select("AuditName = '" + auditDefinition.Name + "'");
                    foreach(DataRow sectionRow in foundRows)
                    {
                        var sectionDefinition = new SectionDefinition();
                        sectionDefinition.Name = (string)sectionRow["Name"];
                        sectionDefinition.Weighting = Convert.ToDouble((string)sectionRow["Weighting"]);
                        sectionDefinition.Rank = Convert.ToInt32((string)sectionRow["Rank"]);
                        sectionDefinition.IsActive = Convert.ToBoolean((string)sectionRow["IsActive"]);
                        sectionDefinition.AuditDefinition = auditDefinition;
                        _context.SectionDefinitions.Add(sectionDefinition);

                        //load the questions for this section
                        foundRows = questionsDt.Select("SectionName = '" + sectionDefinition.Name + "'");
                        foreach (DataRow questionRow in foundRows)
                        {
                            var questionDefinition = new QuestionDefinition
                            {
                                DisplayNumber = (string)questionRow["DisplayNumber"],
                                QuestionText = (string)questionRow["Question"],
                                Guideline = (string)questionRow["Guidelines"],
                                Weight = Convert.ToDouble((string)questionRow["Weight"]),
                                SampleSize = Convert.ToInt32((string)questionRow["SampleSize"]),
                                IsZeroTolerance = Convert.ToBoolean((string)questionRow["IsZeroTolerance"]),
                                ToleranceLimit = Convert.ToInt32((string)questionRow["ToleranceLimit"]),
                                IsBonus = Convert.ToBoolean((string)questionRow["IsBonus"]),
                                IsActive = Convert.ToBoolean((string)questionRow["IsActive"]),
                                Rank = Convert.ToInt32((string)questionRow["Rank"]),
                                PenaltyValue = Convert.ToInt32((string)questionRow["PenaltyValue"]),
                                SectionDefinition = sectionDefinition
                            };
                            _context.QuestionDefinitions.Add(questionDefinition);
                        }
                    }
                } //end foreach-auditdefinition                
                //datastring = StringDataTable(sectionsDt);
                _context.SaveChanges();
            } //end using excel package

            //check if we only have 1 audit, the we redirecto to that audit, else go to index


            if (audit_count == 1 & auditDefinition != null)
                return RedirectToAction("ShowAudit", new { id = auditDefinition.Id });

            return View("Index");
        }

        private static string StringDataTable(DataTable dt)
        {
            string datastring = "<table><thead<tr>";
            foreach (DataColumn sourceColumn in dt.Columns)
            {
                datastring += "<th>" + sourceColumn.ColumnName + "</th>";
            }
            datastring += "</tr></thead><tbody>";

            foreach (DataRow row in dt.Rows)
            {
                datastring += "<tr>";
                foreach (DataColumn sourceColumn in dt.Columns)
                {

                    datastring += "<td>" + row[sourceColumn.ColumnName] + "</td>";
                }
                datastring += "</tr>";
            }
            datastring += "</tbody</table>";
            return datastring;
        }
    } // end class

    
    public static class ExcelPackageExtensions
    {
        public static DataTable ToDataTable(this ExcelPackage package, string sheetName)
        {

            ExcelWorksheet workSheet = null;

            if (String.IsNullOrEmpty(sheetName))
            {
                workSheet = package.Workbook.Worksheets.First();
            }
            else
            {
                workSheet = package.Workbook.Worksheets[sheetName]; //.First();
            }
            
            DataTable table = new DataTable();
            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column])
            {
                table.Columns.Add(firstRowCell.Text);
            }
            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, workSheet.Dimension.End.Column];
                var newRow = table.NewRow();
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text;
                }
                table.Rows.Add(newRow);
            }
            return table;
        }
    }
} //end namespace