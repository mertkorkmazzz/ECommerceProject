using Microsoft.AspNetCore.Mvc;

namespace EcommerceApı.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
