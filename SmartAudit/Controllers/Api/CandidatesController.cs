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
    public class CandidatesController : ApiController
    {
        private ApplicationDbContext _context;
        public MapperConfiguration mapperConfig;
        private IMapper mapper;
        public CandidatesController()
        {
            _context = new ApplicationDbContext();
            mapperConfig = new MapperConfiguration(cfg =>
            {
                // Domain to Dto
                cfg.CreateMap<Candidate, CandidateDto>();

                // Dto to Domain
                cfg.CreateMap<CandidateDto, Candidate>();
            });


            //mapperConfig.AssertConfigurationIsValid();
            mapper = mapperConfig.CreateMapper();

        } //end constructor

        // GET /api/getcandidates
        public IHttpActionResult GetCandidates(string query = null)
        {
            var candidatesQuery = (IEnumerable<Candidate>)_context.Candidates;
            if (!String.IsNullOrWhiteSpace(query))
            {
                //apply the query
                candidatesQuery = _context.Candidates.Where(a => a.Name.Contains(query));
            }
            var candidateDtos = candidatesQuery
                .ToList()
                .Select(mapper.Map<Candidate, CandidateDto>);

            return Ok(candidateDtos);
        } //
    } //end class
} //end namespace
