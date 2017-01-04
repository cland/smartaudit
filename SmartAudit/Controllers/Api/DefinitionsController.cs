using SmartAudit.Dtos;
using SmartAudit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;

namespace SmartAudit.Controllers.Api
{



    public class DefinitionsController : ApiController
    {
        private ApplicationDbContext _context;
        public MapperConfiguration mapperConfig;
        private IMapper mapper;
        public DefinitionsController()
        {
            _context = new ApplicationDbContext();
            mapperConfig = new MapperConfiguration(cfg =>
            {
                // Domain to Dto
                cfg.CreateMap<AuditDefinition, AuditDefinitionDto>();
                cfg.CreateMap<AuditDefinition, AuditDefinitionSimpleDto>();
                cfg.CreateMap<SectionDefinition, SectionDefinitionDto>();
                cfg.CreateMap<QuestionDefinition, QuestionDefinitionDto>();
                cfg.CreateMap<SectionDefinition, SectionDefinitionSimpleDto>();
                cfg.CreateMap<QuestionDefinition, QuestionDefinitionSimpleDto>();

                // Dto to Domain

                cfg.CreateMap< AuditDefinitionDto, AuditDefinition>()
                .ForMember(c => c.Id, opt => opt.Ignore());
                cfg.CreateMap<SectionDefinitionDto, SectionDefinition>()
                .ForMember(c => c.Id, opt => opt.Ignore());
                cfg.CreateMap<QuestionDefinitionDto, QuestionDefinition>()
                .ForMember(c => c.Id, opt => opt.Ignore());


            });


            //mapperConfig.AssertConfigurationIsValid();
            mapper = mapperConfig.CreateMapper();

        }

        /*
         * AUDIT DEFINITION END POINTS 
         * 
         */
        // GET /api/auditdefinitions
        public IHttpActionResult GetAuditDefinitions(string query = null)
        {
            
            if (!String.IsNullOrWhiteSpace(query))
            {
                //apply the query
               //_context.AuditDefinitions.Where(a => a.Name.Contains(query));
            }            
            var auditDefinitionDtos = _context.AuditDefinitions
                .ToList()
                .Select(mapper.Map<AuditDefinition, AuditDefinitionSimpleDto>);

            return Ok(auditDefinitionDtos);
        } //

        // GET /api/definitions/1
        public IHttpActionResult GetAuditDefinition(int id)
        {
            var auditDefinition = _context.AuditDefinitions.SingleOrDefault(a => a.Id == id);
            if (auditDefinition == null) return NotFound();

            return Ok(mapper.Map<AuditDefinition, AuditDefinitionDto>(auditDefinition));
        } //

        // POST /api/definitions
        [HttpPost]
        [Authorize(Roles = RoleName.CanManageDefinitions)]
        public IHttpActionResult CreateAuditDefinition(AuditDefinitionDto auditDefinitionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var auditDefinition = mapper.Map<AuditDefinitionDto, AuditDefinition>(auditDefinitionDto);
            _context.AuditDefinitions.Add(auditDefinition);
            _context.SaveChanges();

            
            auditDefinitionDto.Id = auditDefinition.Id;            
            return Created(new Uri(Request.RequestUri + "/" + auditDefinitionDto.Id), auditDefinitionDto);
        } //

        // PUT /api/definitions/1
        [HttpPut]
        [Authorize(Roles = RoleName.CanManageDefinitions)]
        public void UpdateAuditDefinition(int id, AuditDefinitionDto auditDefinitionDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var auditDefinitionInDb = _context.AuditDefinitions.SingleOrDefault(c => c.Id == id);
            if (auditDefinitionInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);


            mapper.Map(auditDefinitionDto, auditDefinitionInDb);

            _context.SaveChanges();
        } //

        // DELETE /api/definitions/1
        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageDefinitions)]
        public void DeleteAudit(int id)
        {
            var auditDefinitionInDb = _context.AuditDefinitions.SingleOrDefault(c => c.Id == id);
            if (auditDefinitionInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var audit = _context.Audits.FirstOrDefault(a => a.AuditDefinitionId == auditDefinitionInDb.Id);
            if(audit == null)
            {
                //we can delete the audit definition
                _context.AuditDefinitions.Remove(auditDefinitionInDb);
            }
            else
            {
                //set the auditdefinition to in-active
                //TODO: Need to handle this better
                auditDefinitionInDb.IsActive = false;
            }

            
            _context.SaveChanges();
        }


        /*
         * SECTION DEFINITION END POINTS 
         * 
         */
        // GET /api/definitions/auditsections/1
        [Route("api/definitions/auditsections/{id}")]
        public IHttpActionResult GetAuditSectionDefinitions(int id)
        {
            var auditDefinition = _context.AuditDefinitions.SingleOrDefault(a => a.Id == id);

            if (auditDefinition == null) return BadRequest("Audit Definition Id '" + id + "' is invalid");
           
            var sectionDefinitionDtos = auditDefinition.Sections;

            return Ok(sectionDefinitionDtos);
        } //

        // POST /api/definitions/newsection
        [HttpPost]
        [Authorize(Roles = RoleName.CanManageDefinitions)]
        [Route ("api/definitions/newsection")]
        public IHttpActionResult CreateSectionDefinition(SectionDefinitionDto newSection)
        {
            var auditDefinition = _context.AuditDefinitions.SingleOrDefault(a => a.Id == newSection.AuditDefinitionId);
            if (auditDefinition == null) return BadRequest("Audit Id definition is not valid!");

            var sectionDefinition = new SectionDefinition
            {
                Name = newSection.Name,
                Weighting = newSection.Weighting,
                IsActive = newSection.IsActive,
                Rank = newSection.Rank,
                AuditDefinitionId = auditDefinition.Id
            };
            auditDefinition.Sections.Add(sectionDefinition);
            _context.SaveChanges(); // SectionDefinitions.Add(sectionDefinition);
            return Ok();
        } //

        // PUT /api/definitions/updatequestion/1
        [HttpPut]
        [Authorize(Roles = RoleName.CanManageDefinitions)]
        [Route("api/definitions/updatesection/{id}")]
        public void UpdateSection(int id, SectionDefinitionDto sectionDefinitionDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var sectionInDb = _context.SectionDefinitions.SingleOrDefault(c => c.Id == id);
            if (sectionInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);


            mapper.Map(sectionDefinitionDto, sectionInDb);

            _context.SaveChanges();
        }

        /*
         * QUESTION DEFINITION END POINTS 
         * 
         */
        // GET /api/definitions/1
        [Route("api/definitions/question/{id}")]
        public IHttpActionResult GetQuestionDefinition(int id)
        {
            var questionDefinition = _context.QuestionDefinitions.SingleOrDefault(a => a.Id == id);
            if (questionDefinition == null) return NotFound();

            return Ok(mapper.Map<QuestionDefinition, QuestionDefinitionDto>(questionDefinition));
        } //
        // POST /api/definitions/newquestion
        [HttpPost]
        [Authorize(Roles = RoleName.CanManageDefinitions)]
        [Route("api/definitions/newquestion")]
        public IHttpActionResult CreateQuestionDefinition(QuestionDefinitionDto newQuestion)
        {
            var sectionDefinition = _context.SectionDefinitions.SingleOrDefault(a => a.Id == newQuestion.SectionDefinitionId);
            if (sectionDefinition == null) return BadRequest("Section Id definition is not valid!");

            var questionDefinition = new QuestionDefinition
            {
               DisplayNumber = newQuestion.DisplayNumber,
               QuestionText = newQuestion.QuestionText,
               Guideline = newQuestion.Guideline,
               Weight = newQuestion.Weight,
               SampleSize = newQuestion.SampleSize,
               IsZeroTolerance = newQuestion.IsZeroTolerance,
               ToleranceLimit = newQuestion.ToleranceLimit,
               IsBonus = newQuestion.IsBonus,
               IsActive = newQuestion.IsActive,
               Rank = newQuestion.Rank,
               SectionDefinitionId = sectionDefinition.Id
            };
            sectionDefinition.Questions.Add(questionDefinition);
            _context.SaveChanges(); 
            return Ok();
        } //

        // PUT /api/definitions/updatequestion/1
        [HttpPut]
        [Authorize(Roles = RoleName.CanManageDefinitions)]
        [Route("api/definitions/updatequestion/{id}")]
        public void UpdateQuestion(int id, QuestionDefinitionDto questionDefinitionDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var questionInDb = _context.QuestionDefinitions.SingleOrDefault(c => c.Id == id);
            if (questionInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);


            mapper.Map(questionDefinitionDto, questionInDb);

            _context.SaveChanges();
        }
    } //end class
} //end namespace
