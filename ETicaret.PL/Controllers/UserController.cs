using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ETicaret.BL.Business.Abstracts;
using ETicaret.BL.Services;
using ETicaret.Entities.Authentications;
using ETicaret.Entities.Authentications.ViewModels;
using ETicaret.Entities.Authentications.ViewModels.AppUserVM;
using ETicaret.PL.Controllers.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.PL.Controllers
{
    public class UserController : BaseController
    {
        IUserManagerBusiness _userManagerBusiness;
        ISignInManagerBusiness _signInManagerBusiness;
        SignInManager<AppUser> _signInManager;
        UserManager<AppUser> _userManager;
        public  UserController(IUserManagerBusiness userManagerBusiness,
        ISignInManagerBusiness signInManagerBusiness, SignInManager<AppUser> signInManager, UserManager<AppUser>userManager)
        {
            _userManagerBusiness = userManagerBusiness;
            _signInManagerBusiness = signInManagerBusiness;
            _signInManager = signInManager;
            _userManager = userManager;
            
        }
        public IActionResult KayitOl()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>KayitOl(AppUserKayitOlVM model)
        {
            if (!ModelState.IsValid)
                return View(model);
            bool result = await _userManagerBusiness.CreateAsync(model);
            MesajYaz(result, "Kayıt işlemi başarıyla gerçekleştirilmişir.Giriş yapabilirsiniz.", "Kayıt işlemi esnasında beklenmeyen bir hatayla karşılaşılmıştır.Lütfen tekrar deneyiniz.");
            if (!result)
            {
                return View(model);
            }
            else
                return RedirectToAction("SignIn");

        }
        public IActionResult SignIn(string ReturnUrl)
        {
            TempData["ReturnUrl"] = ReturnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>SignIn(AppUserLoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _userManagerBusiness.AddLoginTimeClaim(model);

            var (result, twoFactor) = await _signInManagerBusiness.SignInAsync(model);

            if (result && TempData["ReturnUrl"]!=null)
            {
                return Redirect(TempData["ReturnUrl"].ToString());

            }
            else if (result)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (twoFactor)
            {
                return RedirectToAction("TwoFactorAuthentication", new { ReturnUrl = TempData["ReturnUrl"] });
            }

            MesajYaz(result, "Giriş başarılı.", "Kullanıcı adı/email veya şifre hatalı");
            return RedirectToAction("SignIn");
        }
        public IActionResult TwoFactorAuthentication(string ReturnUrl)
            {
            TempData["ReturnUrl"] = ReturnUrl;
            return View();
            }
        [HttpPost]
        public async Task<IActionResult> TwoFactorAuthentication(TwoFactorLoginVM model)
        {
            AppUser user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            Microsoft.AspNetCore.Identity.SignInResult result = null;
            if (!model.KurtarmaKodumu)
            {
                result = await _signInManager.TwoFactorAuthenticatorSignInAsync(model.DogrulamaKodu, false, false);
            }
            else
            {
                result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(model.DogrulamaKodu);
            }
            if (!result.Succeeded)
            {
                MesajYaz(result.Succeeded, "", "Doğrulama kodunu yanlış girdiniz.Lütfen doğrulama kodunu tekrar giriniz.");
                return View();

            }
            return Redirect(TempData["ReturnUrl"] != null ? TempData["ReturnUrl"].ToString() : Url.Action("Index", "Home"));
        }

        public async Task<IActionResult> Logout() 
        {
            await _signInManagerBusiness.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> SifreTalebi()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>SifreTalebi(AppUserSifreTalepVM model) 
        {
            if (!ModelState.IsValid)
                return View(model);
            var (kullaniciResult, result) = await _userManagerBusiness.MailGonder(model.Email);

            MesajYaz(result, "Mail başarıyla gönderilmiştir. Lütfen email adresinizi kontrol ediniz.", !kullaniciResult ? "Böyle bir email bulunmamaktadır." : "Mail gönderilirken beklenmeyen bir hatayla karşılaşıldı.");
            return View();
        }
        public async Task<IActionResult> SifreGuncelle(int userId, string token) 
        {
            TempData["userId"] = userId;
            TempData["token"] = token;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>SifreGuncelle(AppUserSifreGuncelleVM model) 
        {
            if (!ModelState.IsValid)
                return View(model);
            bool result = await _userManagerBusiness.SifreGuncelle(int.Parse(TempData["userId"].ToString()), TempData["token"].ToString(), model);
            MesajYaz(result, "Şifre güncelleme işlemi başarıyla gerçekleştirilmiştir. Yeni şifrenizi kullanarak giriş yapabilirsiniz.", "Şifre güncelleme işlemi yapılırken beklenmeyen bir hatayla karşılaşıldı.");
            if (result)
                return RedirectToAction("SignIn", "User");
            return View();

        }

        public IActionResult ExternalLogin(string providerName, string ReturnUrl)
        {
            string redirectUrl = Url.Action("ExternalResponse", "User", new { ReturnUrl = ReturnUrl });
            return new ChallengeResult(providerName, _signInManagerBusiness.ExternalLogin(providerName,redirectUrl));
        }
        public async Task<IActionResult> ExternalResponse(string ReturnUrl)
        {
            bool result = await _signInManagerBusiness.ExternalResponse();
            if (!result)
            {
                return RedirectToAction("SignIn", "User");

            }
            else
            {
                return Redirect(ReturnUrl == null ? Url.Action("Index", "Home") : ReturnUrl);
            }
        }
        [Authorize]
        public async Task<IActionResult> ProfilAyarlari()
        {
            AppUserGuncelleVM user = await _userManagerBusiness.GuncellenecekKullaniciGetir(User.Identity.Name);
            return View(user);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProfilAyarlari(AppUserGuncelleVM model)
        {
            bool result = await _userManagerBusiness.KullaniciGuncelle(model);
            MesajYaz(result, "Profil bilgileriniz başarıyla güncellenmiştir.", "Mevcut şifrenizi lütfen doğru giriniz.");
            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> KurtarmaKoduUret()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            var kodlar = (await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 5)).ToList();
            string kurtarmaKodlari = "";
            foreach (var code in kodlar)
            {
                kurtarmaKodlari += code + ",";
            }
            TempData["KurtarmaKodlari"] = kurtarmaKodlari;
            return RedirectToAction("ProfilAyarlari", "User");
        }

    }
}