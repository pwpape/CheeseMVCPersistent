using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.ViewModels
{
    public class AddCheeseViewModel
    {
        [Required]
        [Display(Name = "Cheese Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must give your cheese a description")]
        public string Description { get; set; }

        public CheeseCategory Category { get; set; }

        public int CategoryID { get; set; }

        public List<SelectListItem> Categories { get; set; }

        public AddCheeseViewModel(List<CheeseCategory> list) 
        {

            Categories = new List<SelectListItem>();

            // <option value="0">Hard</option>
            
            foreach (CheeseCategory category in list)
            Categories.Add(new SelectListItem 
            {
                Value = ((int) category.ID).ToString(),
                Text = category.Name.ToString()
            });

        }

        public AddCheeseViewModel()
        {
            
        }
    }
}
