using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Models;
using System.Collections.Generic;
using CheeseMVC.ViewModels;
using CheeseMVC.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {
        private readonly CheeseDbContext context;

        public CheeseController(CheeseDbContext dbContext)
        {
            this.context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            IList<Cheese> cheeses = context.Cheeses.Include(c => c.Category).ToList();

            return View(cheeses);
        }

        private void ToList()
        {
            throw new NotImplementedException();
        }

        public IActionResult Add()
        {
            AddCheeseViewModel addCheeseViewModel = new AddCheeseViewModel(context.Categories.ToList());
            return View(addCheeseViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddCheeseViewModel addCheeseViewModel)
        {
            if (ModelState.IsValid)
            {
                // Add the new cheese to my existing cheeses
                CheeseCategory newCheeseCategory = context.Categories.Single(c => c.ID == addCheeseViewModel.CategoryID);
                
                Cheese newCheese = new Cheese
                {
                    Name = addCheeseViewModel.Name,
                    Description = addCheeseViewModel.Description,
                    Category = newCheeseCategory
                };

                context.Cheeses.Add(newCheese);
                context.SaveChanges();

                return Redirect("/Cheese");
            }

            return View(addCheeseViewModel);
        }

        public IActionResult Remove()
        {
            ViewBag.title = "Remove Cheeses";
            ViewBag.cheeses = context.Cheeses.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Remove(int[] cheeseIds)
        {
            foreach (int cheeseId in cheeseIds)
            {
                Cheese theCheese = context.Cheeses.Single(c => c.ID == cheeseId);
                context.Cheeses.Remove(theCheese);
            }

            context.SaveChanges();

            return Redirect("/");
        }

        public IActionResult Category(int id)
        {
            if (id == 0)
            {
                return Redirect("/Category");
            }

            CheeseCategory theCategory = context.Categories.Include(cat => cat.Cheeses).Single(cat => cat.ID == id);

            ViewBag.title = "Cheeses in  category: " + theCategory.Name;

            return View("Index", theCategory.Cheeses);
        }

        public IActionResult Edit(int cheeseId)
        {
            Cheese cheese = context.Cheeses.Single(c => c.ID == cheeseId);
            CheeseCategory cheeseCategory = context.Categories.Single(cat => cat.ID == cheese.CategoryID);

            EditCheeseViewModel editCheeseViewModel = new EditCheeseViewModel(context.Categories.ToList())
            {
                Cheese = cheese,
                CheeseID = cheese.ID,
                Name = cheese.Name,
                Description = cheese.Description,
                Category = cheeseCategory,
                CategoryID = cheeseCategory.ID
            };
            
            return View(editCheeseViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditCheeseViewModel editCheeseViewModel)
        {
            if (ModelState.IsValid)
            {
            Cheese cheese = context.Cheeses.Single(c => c.ID == editCheeseViewModel.CheeseID);
            cheese.Name = editCheeseViewModel.Name;
            cheese.Description = editCheeseViewModel.Description;
            cheese.CategoryID = editCheeseViewModel.CategoryID;
            context.SaveChanges();
            
            return Redirect("/");
            }


            return View(editCheeseViewModel);
        }
    }
}
