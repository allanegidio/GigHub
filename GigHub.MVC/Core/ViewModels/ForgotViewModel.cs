using System.ComponentModel.DataAnnotations;

namespace GigHub.MVC.Core.ViewModels
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}