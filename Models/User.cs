using Microsoft.AspNetCore.Identity;

namespace GameZone.Models
{
    public class User:IdentityUser
    {
        public string address { get; set; } = default!;
    }
}
