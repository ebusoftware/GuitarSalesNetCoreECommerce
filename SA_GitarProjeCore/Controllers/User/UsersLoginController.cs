using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SA_GitarProjeCore.Data;
using SA_GitarProjeCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SA_GitarProjeCore.Controllers.Admin
{
    public class UsersLoginController : Controller
    {
        private readonly DataContext _context;
        public UsersLoginController(DataContext context)
        {
            _context = context;

        }
        //Bu Controller hariç diğerlerine Authorize işlemi uygula.

        [HttpGet]
        [AllowAnonymous]
        public IActionResult SıgnIn()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SıgnIn(Users p)
        {
            //User tablosundan gelen E_Mail veya kullanıcı adı ve password, Parametreden gelen E_mail veya kullanıcı adı ve password e eşit mi kontrol ediyorum.
            var informations = _context.Users.FirstOrDefault(x => x.E_mail == p.E_mail || x.UserName == p.UserName && x.Password == p.Password);
            if (informations != null)
            {
                //Claims kütüphanesini kullanarak talep oluştuşturuyoruz.. claim tipim parametreden gelen E_mail.
                var claims = new List<Claim>
                    {
                    new Claim(ClaimTypes.Name,p.E_mail)

                    };
                //Useridentity değişkenine ClaimsInentity sınıfından claims Login belirtiyorum.
                var useridentity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                //await komutu Asenkron işlemlerde kullanılır.
                await HttpContext.SignInAsync(principal);
                //Products'daki Index Aksiyonuna yönlendir.
                return RedirectToAction("Index", "Home");

            }
            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/UsersLogin/SıgnIn/");
        }
    }
}
