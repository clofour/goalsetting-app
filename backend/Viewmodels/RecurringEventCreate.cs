using System.ComponentModel.DataAnnotations;
using backend.Config;
using backend.Enums;

namespace backend.Viewmodels
{
    public enum RecurrenceTypes
    {
        DAILY,
        WEEKLY,
        MONTHLY,
        YEARLY
    }

    public enum Weekday
    {
        MO,
        TU,
        WE,
        TH,
        FR,
        SA,
        SU

    }

    public class RecurringEventCreate: EventCreate
    {
        [Required]
        public int RecurrenceAmount { get; set; }
        [Required]
        public RecurrenceTypes RecurrenceType { get; set; }

        [Required]
        public List<Weekday> WeekDays { get; set; }
        [Required]
        public int MonthDay { get; set; }
        [Required]
        public int YearMonth { get; set; }
    }
}