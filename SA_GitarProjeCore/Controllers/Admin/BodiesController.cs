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
    public class BodiesController: Controller
    {
        private readonly DataContext _context;
        public BodiesController(DataContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            List<Bodies> bodys;
            bodys = _context.Bodys.ToList();
            return View(bodys);
        }
        [HttpGet]
        public IActionResult Create()
        {
            Bodies bodys= new Bodies();
            return View(bodys);
        }
        [HttpPost]
        public IActionResult Create(Bodies bodys)
        {
            _context.Add(bodys);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int Id)
        {
            Bodies prod = _context.Bodys.Where(p => p.BodyId== Id).FirstOrDefault();
            return View(prod);
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            Bodies prod = _context.Bodys.Where(p => p.BodyId== Id).FirstOrDefault();
            return View(prod);
        }
        [HttpPost]
        public IActionResult Edit(Bodies bodys)
        {
            _context.Attach(bodys);
            _context.Entry(bodys).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            Bodies prod = _context.Bodys.Where(p => p.BodyId== Id).FirstOrDefault();
            return View(prod);
        }
        [HttpPost]
        public IActionResult Delete(Bodies bodys)
        {
            _context.Attach(bodys);
            _context.Entry(bodys).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
