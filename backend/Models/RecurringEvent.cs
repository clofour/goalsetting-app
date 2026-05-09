using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Config;

namespace backend.Models
{
    public class RecurringEvent(): Event
    {
        public string RRULE { get; set; }
    }
}
