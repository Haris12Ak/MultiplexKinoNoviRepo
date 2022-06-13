using Microsoft.AspNetCore.Mvc;

namespace MultiplexKino.Controllers
{
    public class KontaktController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
