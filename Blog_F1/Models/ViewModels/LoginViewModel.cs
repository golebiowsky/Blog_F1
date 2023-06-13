using System.ComponentModel.DataAnnotations;

namespace Blog_F1.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [MinLength(6, ErrorMessage="Hasło musi zawierać co najmniej 6 znaków")]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
