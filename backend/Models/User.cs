using Microsoft.AspNetCore.Identity;

namespace backend.Models
{
    public class User(string accessCode) : IdentityUser
    {
        public string AccessCode { get; set; } = accessCode;

    }
}
