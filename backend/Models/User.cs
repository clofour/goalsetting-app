using Microsoft.AspNetCore.Identity;

namespace backend.Models
{
    public class User(string accessCode) : IdentityUser<Guid>
    {
        public string AccessCode { get; set; } = accessCode;

        public List<Guid> Inbox { get; set; } = new();
    }
}
