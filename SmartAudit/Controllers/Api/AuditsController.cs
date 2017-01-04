using AutoMapper;
using SmartAudit.Dtos;
using SmartAudit.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Formatting;

namespace SmartAudit.Controllers.Api
{
    public class AuditsController : ApiController
    {
        private ApplicationDbContext _context;
        public MapperConfiguration mapperConfig;
        private IMapper mapper;
        public AuditsController()
        {
            _context = new ApplicationDbContext();
            mapperConfig = new MapperConfiguration(cfg =>
            {
                // Domain to Dto
                cfg.CreateMap<Audit, AuditDto>();
                cfg.CreateMap<Audit, AuditSimpleDto>();
                cfg.CreateMap<Candidate, CandidateDto>();
                cfg.CreateMap<AuditDefinition, AuditDefinitionDto>();
                cfg.CreateMap<AuditDefinition, AuditDefinitionSimpleDto>();                
                cfg.CreateMap<SectionDefinition, SectionDefinitionDto>();
                cfg.CreateMap<SectionDefinition, SectionDefinitionSimpleDto>();
                cfg.CreateMap<QuestionDefinition, QuestionDefinitionDto>();
                cfg.CreateMap<QuestionDefinition, QuestionDefinitionSimpleDto>();
                cfg.CreateMap<SectionDefinitionSimpleDto, SectionResultsDto>();
                cfg.CreateMap<QuestionResult, QuestionResultDto>();
                // Dto to Domain

                cfg.CreateMap<AuditDto, Audit>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.DateCreated, opt => opt.Ignore());
                cfg.CreateMap<AuditSimpleDto, Audit>()                
                .ForMember(c => c.Id, opt => opt.Ignore());
                cfg.CreateMap<CandidateDto, Candidate>()
                .ForMember(c => c.Id, opt => opt.Ignore());
                cfg.CreateMap<QuestionDefinitionSimpleDto, QuestionDefinition>();
            });


            //mapperConfig.AssertConfigurationIsValid();
            mapper = mapperConfig.CreateMapper();

        } //end constructor

        // GET /api/audits
        public IHttpActionResult GetAudits(string query = null)
        {
            var auditsQuery = _context.Audits
                .Include(x => x.AuditDefinition)
                .Include(x => x.Candidate)
                .Include(x => x.AuditStatus)
                .Include(x => x.PeriodType)
                .Include(x=>x.Quarter);
            if (!String.IsNullOrWhiteSpace(query))
            {
                //apply the query
                auditsQuery = _context.Audits.Where(a => a.Candidate.Name.Contains(query));
            }
            var auditDtos = auditsQuery
                .Include(a => a.QuestionResults)
                .ToList()
                .Select(mapper.Map<Audit, AuditSimpleDto>);

            List<AuditSimpleDto> auditResults = new List<AuditSimpleDto>();
            foreach (var audit in auditDtos)
            {
                //build the questionresults here
                List<SectionResultsDto> sectionResults = new List<SectionResultsDto> { };
                var activeSections = audit.AuditDefinition.Sections.Where(s => s.IsActive == true);
                foreach (var section in activeSections)
                {
                    var sectionResultsDto = mapper.Map<SectionDefinitionSimpleDto, SectionResultsDto>(section);
                    var activeQuestions = section.Questions.Where(q => q.IsActive == true);
                    foreach (var question in activeQuestions)
                    {
                        var questionResult = _context.QuestionResults.SingleOrDefault(q => q.Id == question.Id);
                        if (questionResult == null)
                        {
                            questionResult = new QuestionResult
                            {
                                QuestionDefinition = mapper.Map<QuestionDefinitionSimpleDto,QuestionDefinition>(question),
                                QuestionDefinitionId = question.Id,
                                SampleActual = 0
                            };
                        }
                        sectionResultsDto.QuestionResults.Add(mapper.Map<QuestionResult, QuestionResultDto>(questionResult));
                    }

                    audit.SectionResults.Add(sectionResultsDto);                    
                }
                audit.AuditDefinition.Sections = null;
                auditResults.Add(audit);
            } //end for each audit

            return Ok(auditResults);
        } //

        // GET /api/audits/1
        public IHttpActionResult GetAudit(int id)
        {
            var audit = _context.Audits.SingleOrDefault(a => a.Id == id);
            if (audit == null) return NotFound();

            return Ok(mapper.Map<Audit, AuditDto>(audit));
        } //

        // POST /api/audits
        [HttpPost]
        [Authorize(Roles = RoleName.CanManageDefinitions)]
        public IHttpActionResult CreateAudits(AuditDto newAudit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            newAudit.DateCreated = DateTime.Now;
            var audit = mapper.Map<AuditDto, Audit>(newAudit);
            _context.Audits.Add(audit);
            _context.SaveChanges();

            newAudit.Id = audit.Id;
            return Created(new Uri(Request.RequestUri + "/" + newAudit.Id),newAudit);
        } //

        [HttpPut]
        public void UpdateAudit(int id, AuditDto auditDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var auditInDb = _context.Audits.SingleOrDefault(c => c.Id == id);
            if (auditInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            mapper.Map(auditDto, auditInDb);

            _context.SaveChanges();
        }

        [HttpPut]
        [Route("api/audits/updateresults/{id}")]
        public void UpdateAuditResults(int id, AuditResultsDto form)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var auditInDb = _context.Audits.SingleOrDefault(c => c.Id == id);
            if (auditInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            //go through the questions
            foreach(var section in form.sections)
            {
                //var sectionDefinition = _context.SectionDefinitions.SingleOrDefault(s => s.Id == section.SectionId);
                if(section.questions != null)
                {                    
                    foreach(var result in section.questions)
                    {
                        if(result.Id == 0)
                        {
                            //this is a new entry
                            _context.QuestionResults.Add(new QuestionResult
                            {
                               // QuestionDefinition = _context.QuestionDefinitions.SingleOrDefault(q => q.Id == result.QuestionDefinitionId),
                                QuestionDefinitionId = result.QuestionDefinitionId,
                                AuditId = id,
                                SampleActual = result.SampleActual,
                                IsNotApplicable = result.IsNotApplicable,
                                SampleDescription = result.SampleDescription                                
                            });
                        }else
                        {
                            //update existing
                            var questionInDb = _context.QuestionResults.SingleOrDefault(q => q.Id == result.Id);
                            questionInDb.SampleActual = result.SampleActual;
                            questionInDb.IsNotApplicable = result.IsNotApplicable;
                            questionInDb.SampleDescription = result.SampleDescription;
                        }
                        
                    }
                    
                }
            }
            _context.SaveChanges();
            //return Ok(form);
        }

        // DELETE /api/audits/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var auditInDb = _context.Audits.SingleOrDefault(c => c.Id == id);
            if (auditInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _context.Audits.Remove(auditInDb);
            _context.SaveChanges();
        }
    } //end class
} //end namespace
