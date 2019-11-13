using System.ComponentModel.DataAnnotations;

namespace V4_API_Movies_M2M_RepoPattern_EF_CodeFirst_Identity_JWTToken.Controllers
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(100,ErrorMessage="PASSWORD_MIN_LENGTH",MinimumLength=6)]
        public string Password { get; set; }

    }
}