
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SA_GitarProjeCore.Data;
using SA_GitarProjeCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SA_GitarProjeCore.Controllers.Admins
{
    public class AdminsController : Controller
    {
        private readonly DataContext _context;
        public AdminsController(DataContext context)
        {
            _context = context;

        }
        //Login yapılmadıysa login kısmına yönlendirir.
        //[Authorize]
        public IActionResult Index()
        {
            List<Models.Admins> admins;
            admins = _context.Admins.ToList();
            return View(admins);
        }
        [HttpGet]
        public IActionResult Create()
        {
            Models.Admins admins= new Models.Admins();
            return View(admins);
        }
        [HttpPost]
        public IActionResult Create(Models.Admins admins)
        {
            _context.Add(admins);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult Details(int Id)
        {
            Models.Admins prod = _context.Admins.Where(p => p.AdminId == Id).FirstOrDefault();
            return View(prod);
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            Models.Admins prod = _context.Admins.Where(p => p.AdminId == Id).FirstOrDefault();
            return View(prod);
        }
        [HttpPost]
        public IActionResult Edit(Models.Admins admins)
        {
            _context.Attach(admins);
            _context.Entry(admins).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            Models.Admins prod = _context.Admins.Where(p => p.AdminId == Id).FirstOrDefault();
            return View(prod);
        }
        [HttpPost]
        public IActionResult Delete(Models.Admins admins)
        {
            _context.Attach(admins);
            _context.Entry(admins).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
