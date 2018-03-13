using System.ComponentModel.DataAnnotations;

namespace GigHub.MVC.ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}