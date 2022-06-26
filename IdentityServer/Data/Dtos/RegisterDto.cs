using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Data.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required]
        [Compare("Password")]
        public string? RePassword { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
    }
}
