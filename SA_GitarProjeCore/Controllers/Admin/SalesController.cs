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
    public class SalesController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHost;
        public SalesController(DataContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;

        }
        public IActionResult Index()
        {
            List<Sales> sales;
            //ilişkiler
            sales = _context.Sales.Include(u => u.Customers).Include(p => p.Products).ToList();
            return View(sales);
        }
        [HttpGet]
        public IActionResult Create()
        {

            var customers = new List<SelectListItem>();
            var products = new List<SelectListItem>();

            foreach (var pp in _context.Customers)
            {
                customers.Add(new SelectListItem() { Value = pp.CustomerId.ToString(), Text = pp.FirstName+" "+pp.LastName});
            }
            ViewBag.customers = customers;
            foreach (var p in _context.Products)
            {
                products.Add(new SelectListItem() {Value=p.ProductId.ToString(),Text=p.ProductName });
            }
            ViewBag.products=products;


            Sales sales= new Sales();
            return View(sales);
        }
        [HttpPost]

        public IActionResult Create(Sales sales)
        {

            _context.Add(sales);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Details(int Id)
        {
            Sales sales = _context.Sales.Where(p => p.SaleId== Id).Include(u => u.Customers).Include(p => p.Products).FirstOrDefault();
            return View(sales);
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var customers = new List<SelectListItem>();
            var products = new List<SelectListItem>();

            foreach (var pp in _context.Customers)
            {
                customers.Add(new SelectListItem() { Value = pp.CustomerId.ToString(), Text = pp.FirstName + " " + pp.LastName });
            }
            ViewBag.customers = customers;
            foreach (var p in _context.Products)
            {
                products.Add(new SelectListItem() { Value = p.ProductId.ToString(), Text = p.ProductName });
            }
            ViewBag.products = products;
            Sales sales = _context.Sales.Where(p => p.SaleId == Id).Include(u => u.Customers).Include(p => p.Products).FirstOrDefault();

            return View(sales);
        }
        [HttpPost]
        public IActionResult Edit(Sales sales)
        {

            _context.Attach(sales);
            _context.Entry(sales).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            Sales sales = _context.Sales.Where(p => p.SaleId == Id).Include(u => u.Customers).Include(p => p.Products).FirstOrDefault();
            return View(sales);
        }
        [HttpPost]
        public IActionResult Delete(Sales sales)
        {
            _context.Attach(sales);
            _context.Entry(sales).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
