using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MPWebClient.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("api/hello")]
        public string Hello()
        {
            return "Hello world";
        }

        //public IActionResult Error()
        //{
        //    ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        //    return View();
        //}
    }
}
