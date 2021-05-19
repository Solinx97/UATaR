using Microsoft.AspNetCore.Mvc;

namespace UATaR.Controllers
{
    public class ReportController : Controller
    {
        [HttpGet]
        public IActionResult CreateReport()
        {
            return View();
        }
    }
}