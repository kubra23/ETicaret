using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETicaret.BL.Business.Abstracts;
using ETicaret.Entities.Authentications.ViewModels.AppRoleVM;
using ETicaret.PL.Areas.yonetimPaneli.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.PL.Areas.yonetimPaneli.Controllers
{
    [Area("yonetimPaneli")]
    [Authorize(Roles ="Administrator,Moderator,Editor")]
    public class RoleController : BaseController
    {
        IRoleBusiness _roleBusiness;
        public RoleController(IRoleBusiness roleBusiness)
        {
            _roleBusiness = roleBusiness;
        }
        public IActionResult Index()
        {
            return View(_roleBusiness.RolleriGetir());
        }
        public IActionResult RoleEkle() 
        {
            return View();
        }
        [HttpPost]
        //[Authorize(Policy = "CheckLoginTime")]
        public async Task<IActionResult> RoleEkle(AppRoleEkleVM model) 
        {
            if (!ModelState.IsValid)
                return View(model);
            bool result = await _roleBusiness.EkleAsync(model);

            MesajYaz(result, "Rol başarıyla eklenmiştir.", "Rol eklenirken beklenmeyen bir hatayla karşılaşıldı.");

            return RedirectToAction("Index");
        }
        public async Task<IActionResult>Sil(int id) 
        {
            bool result = await _roleBusiness.SilAsync(id);
            MesajYaz(result, "Rol başarıyla silinmiştir.","Rol silinirken beklenmyen bir hatayla karşılaşıldı.");
            return RedirectToAction("Index");
        }
    }
}