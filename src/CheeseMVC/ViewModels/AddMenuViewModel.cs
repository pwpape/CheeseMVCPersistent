using System.ComponentModel.DataAnnotations;

namespace CheeseMVC.Controllers
{
    public class AddMenuViewModel
    {
        [Required]
        [Display(Name="Menu Name")]
        public string Name { get; set; }
        
        
    }
}