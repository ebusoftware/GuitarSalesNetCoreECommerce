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
    public class ColorsController : Controller
    {
            private readonly DataContext _context;
            public ColorsController(DataContext context)
            {
                _context = context;

            }
        public IActionResult Index()
            {
                List<Colors> colors;
                colors = _context.Colors.ToList();
            return View(colors);
            }
        [HttpGet]
            public IActionResult Create()
            {
                Colors colors = new Colors();
                return View(colors);
            }
        [HttpPost]
            public IActionResult Create(Colors colors)
            {
                _context.Add(colors);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        public IActionResult Details(int Id)
            {
                Colors prod = _context.Colors.Where(p => p.ColorId == Id).FirstOrDefault();
                return View(prod);
            }
        [HttpGet]
            public IActionResult Edit(int Id)
            {
                Colors prod = _context.Colors.Where(p => p.ColorId == Id).FirstOrDefault();
                return View(prod);
            }
        [HttpPost]
            public IActionResult Edit(Colors colors)
            {
                _context.Attach(colors);
                _context.Entry(colors).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        [HttpGet]
            public IActionResult Delete(int Id)
            {
                Colors prod = _context.Colors.Where(p => p.ColorId == Id).FirstOrDefault();
                return View(prod);
            }
        [HttpPost]
            public IActionResult Delete(Colors colors)
            {
                _context.Attach(colors);
                _context.Entry(colors).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }

