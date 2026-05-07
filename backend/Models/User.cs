using Microsoft.AspNetCore.Identity;

namespace backend.Models
{
    public class User(string accessCode) : IdentityUser<Guid>
    {
        public string AccessCode { get; set; } = accessCode;

        public List<NorthStar> NorthStars { get; set; } = new List<NorthStar>();
    }
}
