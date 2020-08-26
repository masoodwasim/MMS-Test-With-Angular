using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class MeetingsDto
    {
        public int Id { get; set; }       
        public string Subject { get; set; }       
        public string Agenda { get; set; }       
        public DateTime? DateOfMeeting { get; set; }

        public string Attendees { get; set; }
    
    }
}
