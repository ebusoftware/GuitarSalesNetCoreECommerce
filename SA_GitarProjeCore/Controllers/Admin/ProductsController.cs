using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SA_GitarProjeCore.Data;
using SA_GitarProjeCore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SA_GitarProjeCore.Controllers
{
    public class ProductsController : Controller,IActionFilter
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHost;
        public ProductsController(DataContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;

        }
        public IActionResult Index()
        {
            List<Products> products;
             products= _context.Products.Include(c => c.Categories).Include(b => b.Brands)
                .Include(o => o.Bodys).Include(w => w.Wires).Include(l => l.Colors).ToList();
            return View(products);
        }
        [HttpGet]
        public IActionResult Create()
        {

            var categories = new List<SelectListItem>();
            var brands = new List<SelectListItem>();
            var bodies = new List<SelectListItem>();
            var colors = new List<SelectListItem>();
            var wires = new List<SelectListItem>();
            
            foreach (var pp in _context.Categories)
            {
                categories.Add(new SelectListItem() { Value = pp.CategoryId.ToString(), Text = pp.CategoryName});
            }
            ViewBag.categories = categories;

            foreach (var br in _context.Brands)
            {
                brands.Add(new SelectListItem() { Value = br.BrandId.ToString(), Text= br.BrandName });
            }
            ViewBag.brands = brands;
            foreach (var bo in _context.Bodys)
            {
                bodies.Add(new SelectListItem() { Value = bo.BodyId.ToString(), Text=bo.BodyType});
            }
            ViewBag.bodies =bodies ;
            foreach (var c in _context.Colors)
            {
                colors.Add(new SelectListItem() {Value= c.ColorId.ToString(),Text= c.ColorName });
            }
            ViewBag.colors = colors;
            foreach (var w in _context.Wires)
            {
                wires.Add(new SelectListItem() {Value=w.WiresId.ToString(),Text=w.WiresNumber.ToString() });
            }
            ViewBag.wires = wires;


            Products products= new Products();
            return View(products);
        }
        [HttpPost]
        public IActionResult Create(Products products)
        {

                string uniqueFileName = UploadedFile(products);
                products.ImageName = uniqueFileName;
                _context.Add(products);
                _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Details(int Id)
        {
            Products prod = _context.Products.Where(p => p.ProductId== Id).Include(u => u.Categories)
                .Include(w => w.Wires).Include(co => co.Colors).Include(br =>br.Brands )
                .Include(bo => bo.Bodys).Include(w => w.Wires).FirstOrDefault();
            return View(prod);
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var categories = new List<SelectListItem>();
            var brands = new List<SelectListItem>();
            var bodies = new List<SelectListItem>();
            var colors = new List<SelectListItem>();
            var wires = new List<SelectListItem>();

            foreach (var pp in _context.Categories)
            {
                categories.Add(new SelectListItem() { Value = pp.CategoryId.ToString(), Text = pp.CategoryName });
            }
            ViewBag.categories = categories;

            foreach (var br in _context.Brands)
            {
                brands.Add(new SelectListItem() { Value = br.BrandId.ToString(), Text = br.BrandName });
            }
            ViewBag.brands = brands;
            foreach (var bo in _context.Bodys)
            {
                bodies.Add(new SelectListItem() { Value = bo.BodyId.ToString(), Text = bo.BodyType });
            }
            ViewBag.bodies = bodies;
            foreach (var c in _context.Colors)
            {
                colors.Add(new SelectListItem() { Value = c.ColorId.ToString(), Text = c.ColorName });
            }
            ViewBag.colors = colors;
            foreach (var w in _context.Wires)
            {
                wires.Add(new SelectListItem() { Value = w.WiresId.ToString(), Text = w.WiresNumber.ToString() });
            }
            ViewBag.wires = wires;

            ViewBag.Kategori = categories;
            Products prod = _context.Products.Where(p => p.ProductId == Id).Include(u => u.Categories)
                .Include(w => w.Wires).Include(co => co.Colors).Include(br => br.Brands)
                .Include(bo => bo.Bodys).Include(w => w.Wires).FirstOrDefault();

            return View(prod);
        }
        [HttpPost]
        public IActionResult Edit(Products prod)
        {
            if (prod.ImageFile != null)
            {
                string uniqueFileName = UploadedFile(prod);

                prod.ImageName = uniqueFileName;
            }

            _context.Attach(prod);


            _context.Attach(prod);
            _context.Entry(prod).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            Products prod = _context.Products.Where(p => p.ProductId == Id).Include(u => u.Categories)
                 .Include(w => w.Wires).Include(co => co.Colors).Include(br => br.Brands)
                 .Include(bo => bo.Bodys).Include(w => w.Wires).FirstOrDefault();
            return View(prod);
        }
        [HttpPost]
        public IActionResult Delete(Products products)
        {
            _context.Attach(products);
            _context.Entry(products).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public string UploadedFile(Products model)
        {
            string uniqueFileName = null;


            string uploadsFolder = Path.Combine(_webHost.WebRootPath, "Images/");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                model.ImageFile.CopyTo(fileStream);
            }
            return uniqueFileName;
        }

        public string URLgetir(int Id)
        {

            Products prod = _context.Products.Where(p => p.ProductId == Id).FirstOrDefault();
            string prod1 = prod.ImageName;
            return prod1;
        }



    }

}

