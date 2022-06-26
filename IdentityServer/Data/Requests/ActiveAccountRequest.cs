using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Data.Requests
{
    public class ActiveAccountRequest
    {
        [Required]
        public int userId { get; set; }
        [Required]
        public string? activateCode { get; set; }
    }
}
