using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETicaret.Entities.Authentications;
using ETicaret.PL.Areas.yonetimPaneli.Controllers.Base;
using ETicaret.Entities.Authentications.ViewModels.AppUserVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ETicaret.BL.Business.Abstracts;
using ETicaret.Entities.Authentications.ViewModels.AppRoleVM;

namespace ETicaret.PL.Areas.yonetimPaneli.Controllers
{
    [Authorize()]
    public class UserController : BaseController
    {
        IUserManagerBusiness _userManagerBusiness;
        public UserController(IUserManagerBusiness  userManagerBusiness)
        {
            _userManagerBusiness = userManagerBusiness;
        }
        [Authorize(Roles = "Administrator,Moderator,Editor")]
        public IActionResult Index()
        {
            return View(_userManagerBusiness.AllUser());
        }
        [Authorize(Roles = "Administrator,Moderator,Editor")]
        public async Task<IActionResult>Sil(string id)
        {
            var (result, userName) = await _userManagerBusiness.DeleteAsync(id);
            MesajYaz(result,userName + " isimli kullanıcı silinmiştir.", userName + " isimli kullanıcı silinirken beklenmeyen bir hatayla karşılaşılmıştır.");
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Administrator,Moderator,Editor")]
        public async Task<IActionResult>RoleAta(int id)
        {
            List<AppRoleAtaVM> roller = await _userManagerBusiness.TumRolleriGetir(id);
            return View(roller);
        }
        [HttpPost]
        [Authorize(Roles = "Administrator,Moderator,Editor")]
        public async Task<IActionResult> RoleAta(List<AppRoleAtaVM>model,int id)
         {
            var (result, username) = await _userManagerBusiness.RolAtaveSil(id, model);
            MesajYaz(result, username + "isimli kullanıcıya roller başarıyla atanmıştır.", username + "isimli kullanıcıya rol atanırken beklenmeyen bir hatayla karşılaşıldı. ");
            return RedirectToAction("Index");

         }
        public IActionResult AccessDenied()
        {
            MesajYaz(false, "", "Bu sayfaya erişmeye yada bu işlemi yapmaya yetkiniz bulunmamaktadır.");
            return View();
        }
    }
}