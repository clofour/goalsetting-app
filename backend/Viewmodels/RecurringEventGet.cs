using System.ComponentModel.DataAnnotations;
using backend.Config;
using backend.Enums;

namespace backend.Viewmodels
{
    public class RecurringEventGet: EventGet
    {
        public string RRULE { get; set; }
    }
}
