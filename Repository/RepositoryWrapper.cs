using Contracts;
using Entities;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IMeetingsRepository _meetings;
        private IAttendeesRepository _attendees;
        public IMeetingsRepository Meetings
        {
            get
            {
                if (_meetings == null)
                {
                    _meetings = new MeetingsRepository(_repoContext);
                }
                return _meetings;
            }
        }

        public IAttendeesRepository Attendees
        {
            get
            {
                if (_attendees == null)
                {
                    _attendees = new AttendeesRepository(_repoContext);
                }
                return _attendees;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public async Task SaveAsync()
        {
            await _repoContext.SaveChangesAsync();
        }
    }
}
