using GigHub.MVC.Models;

namespace GigHub.MVC.ViewModels
{
    public class GigDetailsViewModel
    {
        public Gig Gig { get; set; }
        public bool IsAttending { get; set; }
        public Following Following { get; set; }
    }
}