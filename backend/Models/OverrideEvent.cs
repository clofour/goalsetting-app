using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Config;

namespace backend.Models
{
    public class OverrideEvent(): Event
    {
        Guid RecurringEventId { get; set; }
        [ForeignKey("RecurringEventId")]
        RecurringEvent RecurringEvent { get; set; }
        DateTime RecurrenceId { get; set; }
    }
}
