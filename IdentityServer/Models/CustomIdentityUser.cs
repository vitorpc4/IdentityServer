using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Models
{
    public class CustomIdentityUser : IdentityUser<int>
    {
        public DateTime BirthDate { get; set; }
    }
}
