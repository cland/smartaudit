using AutoMapper;
using SmartAudit.Dtos;
using SmartAudit.Models;
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

                // Dto to Domain

                cfg.CreateMap<AuditDto, Audit>()
                .ForMember(c => c.Id, opt => opt.Ignore());
                cfg.CreateMap<AuditSimpleDto, Audit>()                
                .ForMember(c => c.Id, opt => opt.Ignore());
            });


            //mapperConfig.AssertConfigurationIsValid();
            mapper = mapperConfig.CreateMapper();

        } //end constructor

        // GET /api/auditdefinitions
        public IHttpActionResult GetAudits(string query = null)
        {

            if (!String.IsNullOrWhiteSpace(query))
            {
                //apply the query
                //_context.Audits.Where(a => a.Name.Contains(query));
            }
            var auditDtos = _context.Audits
                .ToList()
                .Select(mapper.Map<Audit, AuditSimpleDto>);

            return Ok(auditDtos);
        } //

        // GET /api/definitions/1
        public IHttpActionResult GetAudit(int id)
        {
            var audit = _context.Audits.SingleOrDefault(a => a.Id == id);
            if (audit == null) return NotFound();

            return Ok(mapper.Map<Audit, AuditDto>(audit));
        } //
    } //end class
} //end namespace
