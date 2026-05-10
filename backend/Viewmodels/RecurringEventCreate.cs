using System.ComponentModel.DataAnnotations;
using backend.Config;
using backend.Enums;

namespace backend.Viewmodels
{
    public enum RecurrenceTypes
    {
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
        public int RecurrenceAmount { get; set; }
        public RecurrenceTypes RecurrenceType { get; set; }

        public List<Weekday> WeekDays { get; set; }
        public int MonthDay { get; set; }
        public int YearMonth { get; set; }
    }
}