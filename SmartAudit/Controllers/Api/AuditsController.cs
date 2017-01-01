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

        } //end constructor

        // GET /api/audits
        public IHttpActionResult GetAudits(string query = null)
        {
            var auditsQuery = _context.Audits
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
                .ToList()
                .Select(mapper.Map<Audit, AuditSimpleDto>);

            return Ok(auditDtos);
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
