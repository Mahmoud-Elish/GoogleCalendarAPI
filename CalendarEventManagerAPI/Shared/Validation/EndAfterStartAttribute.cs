using System.ComponentModel.DataAnnotations;

namespace CalendarEventManagerAPI.Shared;

public class EndAfterStartAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var eventModel = (GoogleCalendarDto)validationContext.ObjectInstance;

        if (eventModel.Start >= eventModel.End)
        {
            return new ValidationResult("End date must be after the start date.");
        }

        return ValidationResult.Success;
    }
}
