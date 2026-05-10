using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using backend.Config;
using backend.Enums;

namespace backend.Viewmodels
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(OnetimeEventGet), "onetime")]
    [JsonDerivedType(typeof(RecurringEventGet), "recurring")]
    public abstract class EventGet
    {
        public Guid Id { get; set; }
        public Guid? MovementId { get; set; }

        [MaxLength(FieldLimits.ShortText)]
        public string Title { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
