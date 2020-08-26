using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IMeetingsRepository
    {
        Task<IEnumerable<Meetings>> GetAllMeetingsAsync();
        Task<Meetings> GetMeetingByIdAsync(int MeetingId);
       // Task<Meetings> GetAttendeesWithMeetingIdAsync(int MeetingId);
        void CreateMeeting(Meetings meeting);
        void UpdateMeeting(Meetings meeting);
        void DeleteMeeting(Meetings meeting);
    }
}
