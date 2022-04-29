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
    public class CitiesController : Controller
    {
        private readonly DataContext _context;
        public CitiesController(DataContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            List<Cities> cities;
            cities = _context.Citys.ToList();
            return View(cities);
        }
        [HttpGet]
        public IActionResult Create()
        {
            Cities cities = new Cities();
            return View(cities);
        }
        [HttpPost]
        public IActionResult Create(Cities cities)
        {
            _context.Add(cities);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int Id)
        {
            Cities prod = _context.Citys.Where(p => p.CityId== Id).FirstOrDefault();
            return View(prod);
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            Cities prod = _context.Citys.Where(p => p.CityId== Id).FirstOrDefault();
            return View(prod);
        }
        [HttpPost]
        public IActionResult Edit(Cities cities)
        {
            _context.Attach(cities);
            _context.Entry(cities).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            Cities prod = _context.Citys.Where(p => p.CityId== Id).FirstOrDefault();
            return View(prod);
        }
        [HttpPost]
        public IActionResult Delete(Cities cities)
        {
            _context.Attach(cities);
            _context.Entry(cities).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
