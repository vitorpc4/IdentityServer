using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Data.Requests
{
    public class RequestChangePassword
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Token { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Compare("Password")]
        [Required]
        public string? RePassword { get; set; }
    }
}
