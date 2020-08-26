using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Extensions
{
   public static class OwnerExtensions
    {
        public static void Map(this Meetings dbMeeting, Meetings meeting)
        {
            dbMeeting.Subject = meeting.Subject;
            dbMeeting.Agenda = meeting.Agenda;
            dbMeeting.DateOfMeeting = meeting.DateOfMeeting;
            dbMeeting.Attendees = meeting.Attendees;
        }
    }
}
