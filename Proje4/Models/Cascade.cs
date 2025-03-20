using Microsoft.AspNetCore.Mvc.Rendering;

namespace Proje4.Models
{
    public class Cascade
    {
        public IEnumerable<SelectListItem> KatList { get; set; }
        public IEnumerable<SelectListItem> ParkList { get; set; }

    }
}
