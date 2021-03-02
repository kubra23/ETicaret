using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETicaret.BL.Business.Abstracts;
using ETicaret.PL.Areas.yonetimPaneli.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.PL.Areas.yonetimPaneli.Controllers
{
    [Area("yonetimPaneli")]
    public class YorumController : BaseController
    {
        IYorumBusiness _yorumBusiness;
        public YorumController(IYorumBusiness yorumBusiness)
        {
            _yorumBusiness = yorumBusiness;
        }
        public IActionResult Index()
        {
            return View(_yorumBusiness.TumYorumlar());
        }
        public JsonResult OnayDurumu(bool durum, int yorumId)
        {
            bool result = _yorumBusiness.OnayDurumDegistir(durum, yorumId);
            return Json(result);
        }
        public IActionResult Sil(int id)
        {
            return View();
        }
    }
}