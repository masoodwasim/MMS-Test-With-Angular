using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class AttendeesRepository : RepositoryBase<Attendees>, IAttendeesRepository
    {
        public AttendeesRepository(RepositoryContext repositoryContext)
              : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Attendees>> GetAllAttendeesAsync()
        {
            return await FindAll()
               .OrderBy(ow => ow.FirstName).Where(ow=>ow.IsActive==true)
               .ToListAsync();
        }

        public async Task<Attendees> GetAttendeesByIdAsync(int attendeeId)
        {
            return await FindByCondition(attendee => attendee.Id.Equals(attendeeId))
                .FirstOrDefaultAsync();
        }

        
        public void CreateOwner(Attendees attendee)
        {
            Create(attendee);
        }

        public void UpdateOwner(Attendees attendee)
        {
            Update(attendee);
        }

        public void DeleteOwner(Attendees attendee)
        {
            Delete(attendee);
        }
    }
}
