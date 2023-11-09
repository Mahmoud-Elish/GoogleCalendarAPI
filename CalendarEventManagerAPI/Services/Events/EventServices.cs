using CalendarEventManagerAPI.Shared;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.Calendar.v3.Data;
using Google;
using AutoMapper;

namespace CalendarEventManagerAPI;

public class EventServices : IEventServices
{
    private readonly IMapper _mapper;
    public EventServices(IMapper mapper)
    {
        _mapper = mapper;
    }
    public async Task<EventDto> CreateEventAsync(GoogleCalendarDto eventAdd)
    {
        // Create Google Calendar event
        var newEvent = _mapper.Map<Event>(eventAdd);

        // Insert the event into the user's primary calendar
        EventsResource.InsertRequest request = GoogleCalendarManager().Events.Insert(newEvent, "primary");
        Event createdEvent = await request.ExecuteAsync();
        var dto = _mapper.Map<EventDto>(createdEvent);
        return dto;
    }
    public async Task<IEnumerable<EventDto>> GetFilteredEventsAsync(DateTime? startDate, DateTime? endDate, string? searchQuery)
    {
        // Create list of parameters to filter events
        var listRequest = GoogleCalendarManager().Events.List("primary");

        // (Mapping) optional query parameters 
        listRequest.TimeMin = startDate ?? listRequest.TimeMin;
        listRequest.TimeMax = endDate ?? listRequest.TimeMax;
        listRequest.Q = string.IsNullOrEmpty(searchQuery) ? listRequest.Q : searchQuery;

        // Execute request and get events
        var events = await listRequest.ExecuteAsync();

        // Pagination number of events if large
        var allEvents = new List<Event>();
        while (events.Items != null)
        {
            allEvents.AddRange(events.Items);
            // Check number of pages event
            if (events.NextPageToken == null)
            {
                break;
            }
            // Execute next page of events
            listRequest.PageToken = events.NextPageToken;
            events = await listRequest.ExecuteAsync();
        }
        return allEvents.Select(e => _mapper.Map<EventDto>(e));
    }
    public async Task<bool> DeleteEventAsync(string eventId)
    {
        try
        {
            // Delete Google Calendar event
            await GoogleCalendarManager().Events.Delete("primary", eventId).ExecuteAsync();
            return true; // Event deleted successfully
        }
        catch (GoogleApiException ex) when (ex.HttpStatusCode == System.Net.HttpStatusCode.NotFound)
        {
            // Event not found
            return false;
        }
    }

    private CalendarService GoogleCalendarManager()
    {
        string[] Scopes = { "https://www.googleapis.com/auth/calendar" };
        string ApplicationName = "CanlendarEventManagerAPI";


        UserCredential credential;
        using (var stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "Shared","Credentials", "Credentials.json"), FileMode.Open, FileAccess.Read))
        {
            string credPath = "token.json";
            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                Scopes,
                "user",
                CancellationToken.None,
                new FileDataStore(credPath, true)).Result;
        }

        // Definetion services
        var services = new CalendarService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = ApplicationName,
        });
        return services;
    }
}
