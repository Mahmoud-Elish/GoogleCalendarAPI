using Microsoft.AspNetCore.Mvc;
using Google;
using System.Net;

namespace CalendarEventManagerAPI.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventServices _eventServices;

        public EventsController(IEventServices eventServices)
        {
            _eventServices = eventServices;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] GoogleCalendarDto eventDetails)
        {
            try
            {
                // Validation the input data 
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                }

                var createdEvent = await _eventServices.CreateEventAsync(eventDetails);
                // Return the created event with 201 Created status code
                return Created($"api/events/{createdEvent.Id}", createdEvent);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error occurred while creating the event: " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents(DateTime? startDate, DateTime? endDate, string? searchQuery)
        {
            try
            {
                var filteredEvents = await _eventServices.GetFilteredEventsAsync(startDate, endDate, searchQuery);

                return filteredEvents != null && filteredEvents.Any()
                    ? (IActionResult)Ok(filteredEvents)
                    : NotFound("No events found matching the criteria");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching events: " + ex.Message);
            }
        }

        [HttpDelete]
        [Route("{eventId}")]
        public async Task<IActionResult> DeleteEvent(string eventId)
        {
            if (string.IsNullOrEmpty(eventId))
            {
                return BadRequest("Invalid event ID");
            }
            try
            {
                var isDeleted = await _eventServices.DeleteEventAsync(eventId);

                if (isDeleted)
                {
                    return NoContent(); // Event deleted successfully
                }
                else
                {
                    return NotFound(); // Event not found
                }
            }
            catch (Exception ex)
            {
                if (ex is GoogleApiException googleApiException && googleApiException.HttpStatusCode == HttpStatusCode.Gone)
                {
                    return StatusCode(410, "Resource has been deleted");
                }
                else
                {
                    return StatusCode(500, "An error occurred while deleting the event: " + ex.Message);
                }
            }
        }

    }
}
