using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SA_GitarProjeCore.Data;
using SA_GitarProjeCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SA_GitarProjeCore.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly DataContext _context;
        public CategoriesController(DataContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            List<Categories> categories;
            categories = _context.Categories.ToList();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {

            Categories categories = new Categories();
            return View(categories);
        }
        [HttpPost]
        public IActionResult Create(Categories categories)
        {
            _context.Add(categories);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int Id)
        {
            Categories prod = _context.Categories.Where(p => p.CategoryId== Id).FirstOrDefault();
            return View(prod);
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            Categories prod = _context.Categories.Where(p => p.CategoryId== Id).FirstOrDefault();
            return View(prod);
        }
        [HttpPost]
        public IActionResult Edit(Categories categories)
        {
            _context.Attach(categories);
            _context.Entry(categories).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            Categories prod = _context.Categories.Where(p => p.CategoryId== Id).FirstOrDefault();
            return View(prod);
        }
        [HttpPost]
        public IActionResult Delete(Categories categories)
        {
            _context.Attach(categories);
            _context.Entry(categories).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
