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
    public class BrandsController : Controller
    {

        private readonly DataContext _context;
        public BrandsController(DataContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            List<Brands> brands;
            brands = _context.Brands.ToList();
            return View(brands);
        }
        [HttpGet]
        public IActionResult Create()
        {
            Brands brands= new Brands();
            return View(brands);
        }
        [HttpPost]
        public IActionResult Create(Brands brands)
        {
            _context.Add(brands);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int Id)
        {
            Brands prod = _context.Brands.Where(p => p.BrandId== Id).FirstOrDefault();
            return View(prod);
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            Brands prod = _context.Brands.Where(p => p.BrandId== Id).FirstOrDefault();
            return View(prod);
        }
        [HttpPost]
        public IActionResult Edit(Brands brands)
        {
            _context.Attach(brands);
            _context.Entry(brands).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            Brands prod = _context.Brands.Where(p => p.BrandId== Id).FirstOrDefault();
            return View(prod);
        }
        [HttpPost]
        public IActionResult Delete(Brands brands)
        {
            _context.Attach(brands);
            _context.Entry(brands).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
    }
