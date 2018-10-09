using System.Collections.Generic;
using CheeseMVC.Models;

namespace CheeseMVC.Controllers
{
    public class ViewMenuViewModel
    {
        public Menu Menu { get; internal set; }
        public IList<CheeseMenu> Items { get; set; }

        
    }
}