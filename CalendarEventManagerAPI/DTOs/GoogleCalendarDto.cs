using CalendarEventManagerAPI.Shared;
using System.ComponentModel.DataAnnotations;

namespace CalendarEventManagerAPI;

public record GoogleCalendarDto
{
    [Required(ErrorMessage = "Event summary is required.")]
    public string Summary { get; init; }

    [Required(ErrorMessage = "Event description is required.")]
    public string Description { get; init; }
    [Required(ErrorMessage = "Event Location is required.")]
    public string Location { get; init; }

    [Required(ErrorMessage = "Start date and time is required.")]
    [FutureDate(ErrorMessage = "Event start date cannot be in the past.")]
    [NoFridayOrSaturday(ErrorMessage = "Events cannot be scheduled on Fridays or Saturdays.")]
    public DateTime Start { get; init; }

    [Required(ErrorMessage = "End date and time is required.")]
    [EndAfterStart(ErrorMessage = "End date must be after start date.")]
    [NoFridayOrSaturday(ErrorMessage = "Events cannot be scheduled on Fridays or Saturdays.")]
    public DateTime End { get; init; }
}
