using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects
{
   public class MeetingsForUpdateDto
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Subject is required")]
        [StringLength(60, ErrorMessage = "Subject can't be longer than 60 characters")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Agenda is required")]
        [StringLength(100, ErrorMessage = "Agenda cannot be loner then 100 characters")]
        public string Agenda { get; set; }

        [Required(ErrorMessage = "Attendees is required")]
        public string Attendees { get; set; }

        [Required(ErrorMessage = "Date of meeting is required")]
        public DateTime DateOfMeeting { get; set; }
    }
}
