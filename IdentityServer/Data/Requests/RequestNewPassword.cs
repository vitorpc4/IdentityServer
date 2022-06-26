using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Data.Requests
{
    public class RequestNewPassword
    {
        [Required]
        public string Email { get; set; }
    }
}
