using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETicaret.PL.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.PL.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController()
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}