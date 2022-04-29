using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SA_GitarProjeCore.Data;
using SA_GitarProjeCore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SA_GitarProjeCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;

        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;

        }
        //Login yapılmadıysa login kısmına yönlendirir.
        //[Authorize]
        [AllowAnonymous]
        public IActionResult Index()
        {
            List<Products> products;
            products = _context.Products.Include(c => c.Categories).Include(b => b.Brands)
               .Include(o => o.Bodys).Include(w => w.Wires).Include(l => l.Colors).ToList();
            return View(products);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
