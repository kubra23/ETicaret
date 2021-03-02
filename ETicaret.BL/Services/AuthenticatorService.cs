using ETicaret.Entities.Authentications;
using ETicaret.Entities.Authentications.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ETicaret.BL.Services
{
    public class AuthenticatorService
    {
        UserManager<AppUser> _userManager;
        UrlEncoder _urlEncoder;
        public AuthenticatorService(UserManager<AppUser> userManager, UrlEncoder urlEncoder)
        {
            _urlEncoder = urlEncoder;
            _userManager = userManager;
        }
        public async Task<string> GenerateSharedKey(string userName)
        {
            AppUser user = await _userManager.FindByNameAsync(userName);
            string sharedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            if (string.IsNullOrEmpty(sharedKey))
            {
                IdentityResult result = await _userManager.ResetAuthenticatorKeyAsync(user);
                if (result.Succeeded)
                {
                    sharedKey = await _userManager.GetAuthenticatorKeyAsync(user);
                }

            }
            return sharedKey;
        }
        public async Task<string> GenerateQrCodeUri(string sharedKey, string title, string userName)
        {
            AppUser user = await _userManager.FindByNameAsync(userName);
            return "otpauth://totp/" + _urlEncoder.Encode(title) + ":" + _urlEncoder.Encode(user.Email) + "?secret=" + sharedKey + "&" + "issuer=" + _urlEncoder.Encode(title);
        }
        public async Task<VerifyState>VerifyAsync(AuthenticatorVM model,string userName)
        {
            VerifyState verifyState = new VerifyState();
            AppUser user = await _userManager.FindByNameAsync(userName);
            verifyState.State = await
                _userManager.VerifyTwoFactorTokenAsync(user, _userManager.Options.Tokens.AuthenticatorTokenProvider, model.VerificationCode);
            verifyState.RecoveryCodes = (await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 5)).ToList();
            if (verifyState.State)
            {
                user.TwoFactorEnabled = true;
                await _userManager.UpdateAsync(user);

            }
            return verifyState;
        }
    }
}
