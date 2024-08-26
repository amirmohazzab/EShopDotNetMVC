using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Eshop.Web.Controllers
{
    public class HomeController : Controller
    {
        

        public HomeController()
        {
            
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Contact Us

        [HttpGet("Contact-us")]
        public IActionResult ContactUs()
        {
            return View();
        }

        

        #endregion




    }
}
