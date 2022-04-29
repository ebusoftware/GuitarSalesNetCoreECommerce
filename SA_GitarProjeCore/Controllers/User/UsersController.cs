using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SA_GitarProjeCore.Data;
using SA_GitarProjeCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SA_GitarProjeCore.Controllers.Admin
{
    public class UsersController : Controller
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
            
        }
        public IActionResult Index()
        {
            List<Users> users;
            users = _context.Users.Include(p => p.Citys).ToList();
            return View(users);
        }
        [HttpGet]
        public IActionResult Create()
        {

            var Cities = new List<SelectListItem>();

            foreach (var pp in _context.Citys)
            {
                Cities.Add(new SelectListItem() { Value = pp.CityId.ToString(), Text = pp.CityName });
            }
            ViewBag.cities = Cities;


            Users users= new Users();
            return View(users);
        }
        [HttpPost]

        public IActionResult Create(Users users)
        {

            _context.Add(users);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Details(int Id)
        {
            Users users = _context.Users.Where(p => p.UserId== Id).Include(u => u.Citys).FirstOrDefault();
            return View(users);
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var cities = new List<SelectListItem>();

            foreach (var pp in _context.Citys)
            {
                cities.Add(new SelectListItem() { Value = pp.CityId.ToString(), Text = pp.CityName });
            }
            ViewBag.cities = cities;

            Users users = _context.Users.Where(p => p.UserId== Id).Include(u => u.Citys).FirstOrDefault();

            return View(users);
        }
        [HttpPost]
        public IActionResult Edit(Users users)
        {

            _context.Attach(users);
            _context.Entry(users).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            Users  users = _context.Users.Where(p => p.UserId== Id).Include(u => u.Citys).FirstOrDefault();
            return View(users);
        }
        [HttpPost]
        public IActionResult Delete(Users users)
        {
            _context.Attach(users);
            _context.Entry(users).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
