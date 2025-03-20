using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proje4.Models;

namespace Proje4.Controllers
{
    public class DemoController : Controller
    {
        private readonly OtoparkContext _context;
        public DemoController(OtoparkContext context)
        {
            _context = context;
        }
        Cascade cd = new Cascade();
        public IActionResult Index()
        {
            cd.KatList = new SelectList(_context.Kats, "KatId", "KatNo");
            cd.ParkList = new SelectList(_context.Parks, "ParkId", "ParkAd");
            return View(cd);
        }
        public JsonResult GetParks(int KatId)
        {
            var parklist = (from park in _context.Parks
                            where park.KatId == KatId
                            select new
                            {
                                Text = park.ParkAd,
                                value = park.ParkId
                            }).ToList();
            return Json(parklist, new System.Text.Json.JsonSerializerOptions());
        }
    }
}
