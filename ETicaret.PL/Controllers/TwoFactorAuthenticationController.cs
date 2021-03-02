using ETicaret.BL.Services;
using ETicaret.Entities.Authentications;
using ETicaret.Entities.Authentications.ViewModels;
using ETicaret.PL.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETicaret.PL.Controllers
{
    [Authorize]
    public class TwoFactorAuthenticationController : BaseController
    {
        AuthenticatorService _authenticatorService;
        UserManager<AppUser> _userManager;
        SignInManager<AppUser> _signInManager;

        public TwoFactorAuthenticationController(AuthenticatorService authenticatorService, UserManager<AppUser> userManager,SignInManager<AppUser>signInManager)
        {
            _authenticatorService = authenticatorService;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpPost]
        public IActionResult Sec(TwoFactorTypeSelectVM model) 
        {
            switch (model.TwoFactorType)
            {
                case Entities.Enums.TwoFactorType.Authenticator:
                    return RedirectToAction("AuthenticatorVerify");
                case Entities.Enums.TwoFactorType.SMS:
                    return RedirectToAction("SMSVerify");
                case Entities.Enums.TwoFactorType.Email:
                    return RedirectToAction("EmailVerify");
            }
            return View();
        }
        public async Task<IActionResult> AuthenticatorVerify()
        {
            string sharedKey = await _authenticatorService.GenerateSharedKey(User.Identity.Name);
            string qrCode = await _authenticatorService.GenerateQrCodeUri(sharedKey, "ETicaret", User.Identity.Name);
            MesajYaz(true, "Aşağıdaki barkodu telefonunuzdaki Google ya da Microsoft Authenticator uygulaması ile okutunuz veya yandaki kodu ilgili uygulamaya giriniz.", "");
            return View(new AuthenticatorVM
            {
                QrCodeUri = qrCode,
                SharedKey = sharedKey
            });
        }
        [HttpPost]
        public async Task<IActionResult> AuthenticatorVerify(AuthenticatorVM model)
        {
            VerifyState verifyState = await _authenticatorService.VerifyAsync(model, User.Identity.Name);
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            user.TwoFactorType = Entities.Enums.TwoFactorType.Authenticator;
            await _userManager.UpdateAsync(user);
            await _userManager.UpdateSecurityStampAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            string kurtarmaKodlari = "";
            foreach (var code in verifyState.RecoveryCodes)
            {
                kurtarmaKodlari += code + ",";
            }
            MesajYaz(verifyState.State, "Authenticator ile doğrulama başarıyla onaylanmıştır. <br> Lütfen olası telefonu çaldırma ya da bozulma ihtimaline karşı şu kurtarma kodlarını lütfen kaydediniz : " + kurtarmaKodlari, "Doğrulama kodu yanlış! Lütfen doğrulama kodunu kontrol ediniz.");
            return View(model);

        }
        public async Task<IActionResult>SMSVerify() 
        {
            return View();
        }
        public async Task<IActionResult> EmailVerify()
        {
            return View();
        }
    }
}
