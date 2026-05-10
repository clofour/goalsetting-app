using System.ComponentModel.DataAnnotations;
using backend.Config;
using backend.Enums;

namespace backend.Viewmodels
{
    public class RecurringEventGet: EventGet
    {
        public RecurrenceGet Recurrence { get; set; }
    }

    public class RecurrenceGet
    {
        public string RRULE { get; set; }
        public List<string> ExDate { get; set; }
    }
}
