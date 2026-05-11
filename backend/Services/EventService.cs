using System.ComponentModel;
using AutoMapper;
using backend.Data;
using backend.Enums;
using backend.Models;
using backend.Viewmodels;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class EventService(AppDbContext appDbContext, IMapper mapper)
    {
        public string ConstructRRULE(RecurringEventCreate recurringEventCreate)
        {
            List<string> parts = new List<string>();

            parts.Add($"FREQ={recurringEventCreate.RecurrenceType}");
            parts.Add($"INTERVAL={recurringEventCreate.RecurrenceAmount}");

            switch (recurringEventCreate.RecurrenceType)
            {
                case RecurrenceTypes.WEEKLY:
                    parts.Add($"BYDAY={string.Join(",", recurringEventCreate.WeekDays)}");
                    break;
                case RecurrenceTypes.MONTHLY:
                    parts.Add($"BYMONTHDAY={recurringEventCreate.MonthDay}");
                    break;
                case RecurrenceTypes.YEARLY:
                    parts.Add($"BYMONTHDAY={recurringEventCreate.MonthDay}");
                    parts.Add($"BYMONTH={recurringEventCreate.YearMonth}");
                    break;
            }

            return string.Join(";", parts);
        }

        public DateTime ConstructEnd(DateTime start, int duration)
        {
            return start.AddMinutes(duration);
        }
    }
}