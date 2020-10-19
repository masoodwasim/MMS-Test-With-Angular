using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMSTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingsController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        public MeetingsController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMeetings()
        {
            try
            {
                var meetings = await _repository.Meetings.GetAllMeetingsAsync();
                _logger.LogInfo($"Returned all meetings from database.");

                var meetingsResult = _mapper.Map<IEnumerable<MeetingsDto>>(meetings);
                
                return Ok(meetingsResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllMeetings action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        //https://localhost:44335/api/meetings/2
        [HttpGet("{id}", Name = "MeetingById")]
        public async Task<IActionResult> GetMeetingById(int id)
        {
            try
             {
                var meeting = await _repository.Meetings.GetMeetingByIdAsync(id);
                if (meeting == null)
                {
                    _logger.LogError($"Meeting with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned meeting with id: {id}");

                    var meetingResult = _mapper.Map<MeetingsDto>(meeting);
                    return Ok(meetingResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetMeetingById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        //[HttpGet("{id}/account")]
        //public async Task<IActionResult> GetOwnerWithDetails(Guid id)
        //{
        //    try
        //    {
        //        var owner = await _repository.Owner.GetOwnerWithDetailsAsync(id);
        //        if (owner == null)
        //        {
        //            _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
        //            return NotFound();
        //        }
        //        else
        //        {
        //            _logger.LogInfo($"Returned owner with details for id: {id}");

        //            var ownerResult = _mapper.Map<OwnerDto>(owner);
        //            return Ok(ownerResult);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Something went wrong inside GetOwnerWithDetails action: {ex.Message}");
        //        return StatusCode(500, "Internal server error");
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> CreateMeeting([FromBody]MeetingsForCreationDto meeting)
        {
            try
            {
                if (meeting == null)
                {
                    _logger.LogError("Meeting object sent from client is null.");
                    return BadRequest("Meeting object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid meeting object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var meetingEntity = _mapper.Map<Meetings>(meeting);
                var attendees = new List<string>();
                foreach(var item in meeting.Attendees)
                    attendees.Add(item.FullName);
                meetingEntity.Attendees = String.Join(";", attendees);
                _repository.Meetings.CreateMeeting(meetingEntity);
                await _repository.SaveAsync();

                var createdMeeting = _mapper.Map<MeetingsDto>(meetingEntity);

                return CreatedAtRoute("MeetingById", new { id = createdMeeting.Id }, createdMeeting);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateMeeting action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMeeting([FromBody]MeetingsForUpdateDto meeting)
        {
            try
            {
                if (meeting == null)
                {
                    _logger.LogError("Meeting object sent from client is null.");
                    return BadRequest("Meeting object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid meeting object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var meetingEntity = await _repository.Meetings.GetMeetingByIdAsync(meeting.Id);
                if (meetingEntity == null)
                {
                    _logger.LogError($"Meeting with id: {meeting.Id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(meeting, meetingEntity);
                var attendees = new List<string>();
                foreach (var item in meeting.Attendees)
                    attendees.Add(item.FullName);
                meetingEntity.Attendees = String.Join(";", attendees);
                _repository.Meetings.UpdateMeeting(meetingEntity);
                await _repository.SaveAsync();

                var createdMeeting = _mapper.Map<MeetingsDto>(meetingEntity);

                return CreatedAtRoute("MeetingById", new { id = createdMeeting.Id }, createdMeeting);
                //return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateMeeting action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}"), ActionName("Delete")]
        public async Task<IActionResult> DeleteMeeting(int id)
        {
            try
            {
                var meeting = await _repository.Meetings.GetMeetingByIdAsync(id);
                if (meeting == null)
                {
                    _logger.LogError($"Meeting with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.Meetings.DeleteMeeting(meeting);
                await _repository.SaveAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteMeeting action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}