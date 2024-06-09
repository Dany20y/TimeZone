using System.ComponentModel.DataAnnotations;

namespace Time_Zone.Models
{
    public class PasswordChangeModel
    {
        // Adăugați aici proprietățile și metodele necesare
        [Required]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The new password must be at least {2} characters long.", MinimumLength = 6)]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
