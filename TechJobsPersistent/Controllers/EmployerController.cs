using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechJobsPersistent.Data;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechJobsPersistent.Controllers
{
    public class EmployerController : Controller
    {
        private JobDbContext context;

        public EmployerController(JobDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Employer> employers = context.Employers.ToList();
            return View(employers);
        }

        public IActionResult Add()
        {
            AddEmployerViewModel viewModel = new AddEmployerViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ProcessAddEmployerForm(AddEmployerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string name = viewModel.Name;
                string location = viewModel.Location;

                List<Employer> currentItems = context.Employers
                    .Where(e => e.Name == name)
                    .Where(e => e.Location == location)
                    .ToList();

                if (currentItems.Count == 0)
                {
                    Employer employer = new Employer
                    {
                        Name = name,
                        Location = location
                    };

                    context.Employers.Add(employer);
                    context.SaveChanges();
                }

                return Redirect("/Home/Detail" + name);
            }

            return View(viewModel);
        }

        public IActionResult About(int id)
        {
            List<Employer> employers = context.Employers
                .Where(e => e.Id == id)
                .Include(e => e.Name)
                .Include(e => e.Location)
                .ToList();

            return View(employers);
        }
    }
}
