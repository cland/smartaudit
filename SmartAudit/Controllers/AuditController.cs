using SmartAudit.Models;
using SmartAudit.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Collections;
using SmartAudit.Dtos;
using AutoMapper;

namespace SmartAudit.Controllers
{
    public class AuditController : Controller
    {
        private ApplicationDbContext _context;
        private static string _AuditForm = "AuditForm";
        private static string _ShowAudit = "ShowAudit";
        public MapperConfiguration mapperConfig;
        private IMapper mapper;

        public AuditController()
        {
            _context = new ApplicationDbContext();
            mapperConfig = new MapperConfiguration(cfg =>
            {
                // Domain to Dto
                cfg.CreateMap<Audit, AuditDto>();
                cfg.CreateMap<Audit, AuditSimpleDto>();
                cfg.CreateMap<AuditDefinition, AuditDefinitionSimpleDto>();
                cfg.CreateMap<SectionDefinition, SectionDefinitionSimpleDto>();
                cfg.CreateMap<QuestionDefinition, QuestionDefinitionSimpleDto>();
                cfg.CreateMap<Candidate, CandidateDto>();
                cfg.CreateMap<QuestionResult, QuestionResultDto>();
                cfg.CreateMap<SectionDefinition, SectionResultsDto>();

                // Dto to Domain

                cfg.CreateMap<AuditDto, Audit>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.DateCreated, opt => opt.Ignore());
                cfg.CreateMap<AuditSimpleDto, Audit>()
                .ForMember(c => c.Id, opt => opt.Ignore());
                cfg.CreateMap<CandidateDto, Candidate>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            });


            //mapperConfig.AssertConfigurationIsValid();
            mapper = mapperConfig.CreateMapper();
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

            var audit = new Audit
            {
                AuditDefinitionId = auditDefinition.Id
            };
            var auditSimpleDto = mapper.Map<Audit, AuditSimpleDto>(audit);
            var viewModel = new AuditViewModel
            {
                AuditDefinition = auditDefinition,
                Audit = auditSimpleDto,
                AuditDefinitions = _context.AuditDefinitions.ToList(),
                PeriodTypes = _context.PeriodTypes.ToList(),
                AuditStates = _context.AuditStates.ToList(),
                Quarters = _context.Quarters.ToList()
            };
            return View(_AuditForm, viewModel);
        } //

        [Authorize(Roles = RoleName.CanManageDefinitions)]
        public ActionResult EditAudit(int Id)
        {
            var audit = _context.Audits
                .Include(a => a.Candidate)
                .Include(a => a.PeriodType)
                .Include(a => a.Quarter)
                .Include(a => a.AuditStatus)
                .SingleOrDefault(a => a.Id == Id);
            if(audit == null) return HttpNotFound("Audit with id '" + Id + "' not found");
            var auditDefinition = _context.AuditDefinitions
                .Include(a => a.Sections)
                .SingleOrDefault(a => a.Id == audit.AuditDefinitionId);

            List<SectionResultsDto> sectionResults = new List<SectionResultsDto> { };
            var activeSections = auditDefinition.Sections.Where(s => s.IsActive == true);
            foreach (var section in activeSections)
            {
                var sectionResultsDto = mapper.Map<SectionDefinition, SectionResultsDto>(section);
                var activeQuestions = section.Questions.Where(q => q.IsActive == true);
                foreach (var question in activeQuestions)
                {
                    var questionResult = _context.QuestionResults.SingleOrDefault(q => q.QuestionDefinitionId == question.Id);
                    if (questionResult == null)
                    {
                        questionResult = new QuestionResult
                        {
                            QuestionDefinition = question,
                            QuestionDefinitionId = question.Id,
                            SampleActual = 0
                        };
                    }
                    else
                    {
                        questionResult.QuestionDefinition = question;
                    }
                    sectionResultsDto.QuestionResults.Add(mapper.Map<QuestionResult, QuestionResultDto>(questionResult));
                }

                sectionResults.Add(sectionResultsDto);
            }
            var auditSimpleDto = mapper.Map<Audit, AuditSimpleDto>(audit);
            auditSimpleDto.SectionResults = sectionResults;
            var viewModel = new AuditViewModel
            {
                AuditDefinition = auditDefinition,
                Audit = auditSimpleDto,
                //SectionResults = sectionResults,
                AuditDefinitions = null,
                PeriodTypes = _context.PeriodTypes.ToList(),
                AuditStates = _context.AuditStates.ToList(),
                Quarters = _context.Quarters.ToList()
            };
            return View(_AuditForm, viewModel);
        }
        public ActionResult ShowAudit(int id)
        {
            var audit = _context.Audits
                .Include(a => a.AuditDefinition)
                .SingleOrDefault(a => a.Id == id);
            if (audit == null) return HttpNotFound();

            //build the questionresults here
            List<SectionResultsDto> sectionResults = new List<SectionResultsDto> { };
            var activeSections = audit.AuditDefinition.Sections.Where(s => s.IsActive == true);
            foreach (var section in activeSections)
            {
                var sectionResultsDto = mapper.Map<SectionDefinition, SectionResultsDto>(section);
                var activeQuestions = section.Questions.Where(q => q.IsActive == true);
                foreach (var question in activeQuestions)
                {
                    var questionResult = _context.QuestionResults.SingleOrDefault(q => q.QuestionDefinitionId == question.Id);
                    if (questionResult == null)
                    {
                        questionResult = new QuestionResult
                        {
                            QuestionDefinition = question,
                            QuestionDefinitionId = question.Id,
                            SampleActual = 0
                        };
                    }else
                    {
                        questionResult.QuestionDefinition = question;
                    }
                    sectionResultsDto.QuestionResults.Add(mapper.Map<QuestionResult,QuestionResultDto>(questionResult));
                }
               
                sectionResults.Add(sectionResultsDto);
            }
            var auditSimpleDto = mapper.Map<Audit, AuditSimpleDto>(audit);
            auditSimpleDto.SectionResults = sectionResults;
            var viewModel = new ShowAuditViewModel
            {
                //SectionResults = sectionResults,
                Audit = auditSimpleDto
            };
            return View(_ShowAudit, viewModel);
        }

        
    } //end class
} //end namespace