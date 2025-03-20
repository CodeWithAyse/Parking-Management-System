using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Proje4.Models;

namespace Proje4.Controllers
{
    public class StartPageController : Controller
    {
        private readonly OtoparkContext _context;
        public StartPageController(OtoparkContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            ClaimsPrincipal claimuser = HttpContext.User;
            if (claimuser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(KullaniciBilgisi kullanicibilgisi)
        {
            //var kullanici = _context.KullaniciBilgisi.Include(m => m.KullaniciAd == "Adogan" && m.KullaniciSifre == "157894");
            var kullanici = await _context.KullaniciBilgisi.FirstOrDefaultAsync(m => m.KullaniciAd == kullanicibilgisi.KullaniciAd && m.KullaniciSifre == kullanicibilgisi.KullaniciSifre);
            if (kullanici != null)
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, kullanicibilgisi.KullaniciAd),
                    new Claim("Diğer özellikler","Örnek Rol")

                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties prop = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    //IsPersistent = kullanicibilgisi.LoggedStatus
                    IsPersistent = false
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), prop);
                return RedirectToAction("Index", "Home");
            }

            ViewData["OnayMesaji"] = "Kullanıcı bulunamadı";
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            var prop = new AuthenticationProperties
            {
                IsPersistent = false
            };
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "StartPage");
        }
    }
}
