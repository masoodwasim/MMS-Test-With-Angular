using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MMSTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendeesController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        public AttendeesController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAttendess()
        {
            try
            {
                var attendees = await _repository.Attendees.GetAllAttendeesAsync();
                _logger.LogInfo($"Returned all attendees from database.");

                var attendeesResult = _mapper.Map<IEnumerable<AttendeesDto>>(attendees);

                return Ok(attendeesResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllAttendess action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}