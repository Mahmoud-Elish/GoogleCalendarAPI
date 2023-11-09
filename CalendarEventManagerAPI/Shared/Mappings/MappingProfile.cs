using AutoMapper;
using Google.Apis.Calendar.v3.Data;

namespace CalendarEventManagerAPI.Shared;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<EventDateTime, DateTime>().ConvertUsing(src => src.DateTime ?? DateTime.MinValue);
        CreateMap<Event, EventDto>();
        CreateMap<GoogleCalendarDto, Event>()
           .ForMember(dest => dest.Start, opt => opt.MapFrom(src => new EventDateTime
           {
               DateTime = src.Start,
               TimeZone = "Africa/Cairo"
           }))
           .ForMember(dest => dest.End, opt => opt.MapFrom(src => new EventDateTime
           {
               DateTime = src.End,
               TimeZone = "Africa/Cairo"
           }));
    }
}
