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

                // Dto to Domain
                
                cfg.CreateMap< AuditDefinitionDto, AuditDefinition>()
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
                .Select(mapper.Map<AuditDefinition, AuditDefinitionDto>);

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
            auditDefinitionDto.Sections.Add(new SectionDefinition
            {
                Name = "Default Section",
                Weighting = 1,
                Order = 1,
                IsActive = true
            });
            var auditDefinition = Mapper.Map<AuditDefinitionDto, AuditDefinition>(auditDefinitionDto);
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


            Mapper.Map(auditDefinitionDto, auditDefinitionInDb);

            _context.SaveChanges();
        } //

        // DELETE /api/definitions/1
        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageDefinitions)]
        public void DeleteCustomer(int id)
        {
            var auditDefinitionInDb = _context.AuditDefinitions.SingleOrDefault(c => c.Id == id);
            if (auditDefinitionInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _context.AuditDefinitions.Remove(auditDefinitionInDb);
            _context.SaveChanges();
        }


        /*
         * SECTION DEFINITION END POINTS 
         * 
         */
        // GET

        // POST /api/definitions
        [HttpPost]
        [Authorize(Roles = RoleName.CanManageDefinitions)]
        [Route ("api/definitions/newsection")]
        public IHttpActionResult CreateSectionDefinition(NewSectionDto newSection)
        {
            var auditDefinition = _context.AuditDefinitions.SingleOrDefault(a => a.Id == newSection.AuditDefinitionId);
            if (auditDefinition == null) return BadRequest("Audit Id definition is not valid!");

            var sectionDefinition = new SectionDefinition
            {
                Name = newSection.Name,
                Weighting = newSection.Weighting,
                IsActive = newSection.IsActive,
                Order = newSection.Order
            };
            auditDefinition.Sections.Add(sectionDefinition);
            _context.SaveChanges(); // SectionDefinitions.Add(sectionDefinition);
            return Ok();
        } //

        /*
         * QUESTION DEFINITION END POINTS 
         * 
         */
        // GET
    } //end class
} //end namespace
