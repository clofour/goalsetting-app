using Microsoft.AspNetCore.Identity;

namespace backend.Models
{
    public class User(string accessCode) : IdentityUser
    {
        public string AccessCode { get; set; } = accessCode;

        public List<NorthStar> Goals {get; set; } = new List<NorthStar>();
    }
}
