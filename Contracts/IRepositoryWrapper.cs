using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IMeetingsRepository Meetings { get; }
        IAttendeesRepository Attendees { get; }
        Task SaveAsync();
    }
}
