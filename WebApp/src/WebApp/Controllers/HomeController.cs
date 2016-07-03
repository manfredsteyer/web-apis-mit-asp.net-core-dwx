using FlugDemo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SummitDemo.Controllers
{
    
    public class HomeController: Controller
    {
        private IFlightRepository repo;

        public HomeController() {
        }

        public IActionResult Index()
        {
            return View("Index");
        }
        
        /*
        public HomeController(IFlightRepository repo)
        {
            this.repo = repo;
        }


        public IActionResult List() {
            var all = this.repo.FindAll();
            return View("List", all); 
        }

        public IActionResult Detail(int id) {
            var flug = this.repo.FindById(id);
            return View("Detail", flug);
        }

        public IActionResult About() {
            return View();
        }

        public IActionResult Contact() {
            return View();
        }
        */
    }
}
