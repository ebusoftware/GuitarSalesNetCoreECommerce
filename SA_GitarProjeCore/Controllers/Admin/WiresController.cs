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
    public class WiresController : Controller
    {
        private readonly DataContext _context;
        public WiresController(DataContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            List<Wires> wires;
            wires = _context.Wires.ToList();
            return View(wires);
        }
        [HttpGet]
        public IActionResult Create()
        {
            Wires wires = new Wires();
            return View(wires);
        }
        [HttpPost]
        public IActionResult Create(Wires wires)
        {
            _context.Add(wires);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int Id)
        {
            Wires prod = _context.Wires.Where(p => p.WiresId == Id).FirstOrDefault();
            return View(prod);
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            Wires prod = _context.Wires.Where(p => p.WiresId == Id).FirstOrDefault();
            return View(prod);
        }
        [HttpPost]
        public IActionResult Edit(Wires wires)
        {
            _context.Attach(wires);
            _context.Entry(wires).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            Wires prod = _context.Wires.Where(p => p.WiresId == Id).FirstOrDefault();
            return View(prod);
        }
        [HttpPost]
        public IActionResult Delete(Wires wires)
        {
            _context.Attach(wires);
            _context.Entry(wires).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
