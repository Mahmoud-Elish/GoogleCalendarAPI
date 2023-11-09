using System.ComponentModel.DataAnnotations;

namespace CalendarEventManagerAPI.Shared;

public class NoFridayOrSaturdayAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value is DateTime date)
        {
            return date.DayOfWeek != DayOfWeek.Friday && date.DayOfWeek != DayOfWeek.Saturday;
        }
        return false;
    }
}
