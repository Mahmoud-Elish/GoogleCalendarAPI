namespace CalendarEventManagerAPI;

public interface IEventServices
{
    Task<EventDto> CreateEventAsync(GoogleCalendarDto eventAdd);
    Task<IEnumerable<EventDto>> GetFilteredEventsAsync(DateTime? startDate, DateTime? endDate, string? searchQuery);
    Task<bool> DeleteEventAsync(string eventId);
}
