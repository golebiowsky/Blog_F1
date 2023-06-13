using System.ComponentModel.DataAnnotations;

namespace Blog_F1.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6, ErrorMessage ="Hasło musi składać się z co najmniej 6 znaków")]
        public string Password { get; set; }
    }
}
