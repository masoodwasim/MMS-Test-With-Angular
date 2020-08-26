using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAttendeesRepository
    {
        Task<IEnumerable<Attendees>> GetAllAttendeesAsync();
        Task<Attendees> GetAttendeesByIdAsync(int attendeeId);
        void CreateOwner(Attendees attendee);
        void UpdateOwner(Attendees attendee);
        void DeleteOwner(Attendees attendee);
    }
}
