using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace MMSTest.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Meetings, MeetingsDto>();

            CreateMap<MeetingsForCreationDto, Meetings>();

            CreateMap<MeetingsForUpdateDto, Meetings>();

            CreateMap<Attendees, AttendeesDto>();

            CreateMap<AttendeesDto, AttendeesListDto>();


        }
    }
}
