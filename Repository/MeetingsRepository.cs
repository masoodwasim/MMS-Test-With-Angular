using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class MeetingsRepository : RepositoryBase<Meetings>, IMeetingsRepository
    {
        public MeetingsRepository(RepositoryContext repositoryContext)
               : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Meetings>> GetAllMeetingsAsync()
        {
            return  await FindAll()
               .OrderByDescending(meeting => meeting.Id)
               .ToListAsync();
          
        }

        public async Task<Meetings> GetMeetingByIdAsync(int meetingId)
        {
            return await FindByCondition(meeting => meeting.Id.Equals(meetingId))
                .FirstOrDefaultAsync();
        }

        //public async Task<Meetings> GetAttendeesWithMeetingIdAsync(int meetingId)
        //{
        //    return await FindByCondition(meeting => meeting.Id.Equals(meetingId))
        //        .Include(ac => ac.Attendees)
        //        .FirstOrDefaultAsync();
        //}

        public void CreateMeeting(Meetings meeting)
        {
            Create(meeting);
        }

        public void UpdateMeeting(Meetings meeting)
        {
            Update(meeting);
        }

        public void DeleteMeeting(Meetings meeting)
        {
            Delete(meeting);
        }
    }
}
