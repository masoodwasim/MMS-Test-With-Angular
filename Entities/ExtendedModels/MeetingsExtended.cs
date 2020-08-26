using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ExtendedModels
{
    public class MeetingsExtended
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Agenda { get; set; }
        public DateTime DateOfMeeting { get; set; }

        public string Attendees { get; set; }
        public IEnumerable<Attendees> AttendeesList { get; set; }

        public MeetingsExtended()
        {
        }

        public MeetingsExtended(Meetings meeting)
        {
            Id = meeting.Id;
            Subject = meeting.Subject;
            Agenda = meeting.Agenda;
            Attendees = meeting.Attendees;
            DateOfMeeting = meeting.DateOfMeeting;            
        }
    }
}
