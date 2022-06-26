using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Data.Requests
{
    public class RequestLogin
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Compare("Password")]
        [Required]
        public string RePassword { get; set; }
    }
}
