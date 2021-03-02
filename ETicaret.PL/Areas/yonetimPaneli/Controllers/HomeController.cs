using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETicaret.PL.Areas.yonetimPaneli.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.PL.Areas.yonetimPaneli.Controllers
{
    [Area("yonetimPaneli")]
    [Authorize(Roles = "Administrator,Moderator,Editor")]
    public class HomeController : BaseController
    {
       
        public IActionResult Index()
        {
            return View();
        }
    }
}