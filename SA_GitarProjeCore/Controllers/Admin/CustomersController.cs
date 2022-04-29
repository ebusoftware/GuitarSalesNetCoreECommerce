using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SA_GitarProjeCore.Data;
using SA_GitarProjeCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SA_GitarProjeCore.Controllers
{
    public class CustomersController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHost;
        public CustomersController(DataContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;

        }
        public IActionResult Index()
        {
            List<Customers> customers;
            customers = _context.Customers.Include(p => p.Citys).ToList();
            return View(customers);
        }
        [HttpGet]
        public IActionResult Create()
        {

            var Cities= new List<SelectListItem>();

            foreach (var pp in _context.Citys)
            {
                Cities.Add(new SelectListItem() { Value = pp.CityId.ToString(), Text = pp.CityName});
            }
            ViewBag.cities = Cities;


            Customers customers= new Customers();
            return View(customers);
        }
        [HttpPost]

        public IActionResult Create(Customers customers)
        {

            _context.Add(customers);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Details(int Id)
        {
            Customers customers= _context.Customers.Where(p => p.CustomerId == Id).Include(u => u.Citys).FirstOrDefault();
            return View(customers);
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var cities= new List<SelectListItem>();

            foreach (var pp in _context.Citys)
            {
                cities.Add(new SelectListItem() { Value = pp.CityId.ToString(),Text=pp.CityName});
            }
            ViewBag.cities = cities;

            Customers customers= _context.Customers.Where(p => p.CustomerId== Id).Include(u => u.Citys).FirstOrDefault();

            return View(customers);
        }
        [HttpPost]
        public IActionResult Edit(Customers customers)
        {

            _context.Attach(customers);
            _context.Entry(customers).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            Customers customers= _context.Customers.Where(p => p.CustomerId== Id).Include(u => u.Citys).FirstOrDefault();
            return View(customers);
        }
        [HttpPost]
        public IActionResult Delete(Customers customers)
        {
            _context.Attach(customers);
            _context.Entry(customers).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
