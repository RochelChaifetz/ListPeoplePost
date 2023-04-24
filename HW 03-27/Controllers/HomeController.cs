using HW_03_27.Models;
using ListPeoplePost;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HW_03_27.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString = @"Data Source=.\sqlexpress; Initial Catalog=People; Integrated Security=true;";

        public IActionResult Index()
        {
            return Redirect("/home/showpeople");
        }
        public IActionResult ShowPeople()
        {
            var db = new PersonManager(_connectionString);
            var vm = new PeopleViewModel
            {
                People = db.GetAll(),
            };
            if (TempData["message"] != null)
            {
                vm.Message = (string)TempData["message"];
            }
            return View(vm);
        }

        public IActionResult ShowAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPerson(List<Person> people)
        {
            var db = new PersonManager(_connectionString);
            db.AddMany(people);
            if (people.Count > 1)
            {
                TempData["message"] = $"{people.Count} people have been added!";
            }
            else
            {
                TempData["message"] = $"{people.Count} person has been added!";
            }
            return Redirect("/home/ShowPeople");
        }

    }
}