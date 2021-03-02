using ETicaret.BL.Business.Abstracts;
using ETicaret.BL.Services;
using ETicaret.Entities.Authentications;
using ETicaret.Entities.Authentications.ViewModels.AppUserVM;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.BL.Business
{
    public class SignInManagerBusiness : ISignInManagerBusiness
    {
        SignInManager<AppUser> _signInManager;
        UserManager<AppUser> _userManager;
        public SignInManagerBusiness(SignInManager<AppUser> signInManager, UserManager<AppUser>userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;

        }
        public AuthenticationProperties ExternalLogin(string providerName, string redirectUrl)
        {
            AuthenticationProperties authenticationProperties = _signInManager.ConfigureExternalAuthenticationProperties(providerName, redirectUrl);
            return authenticationProperties;
        }

        public async Task<bool> ExternalResponse()
        {
            ExternalLoginInfo externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync();
            if (externalLoginInfo==null)
            {
                return false;
            }
            else
            {
                AppUser user = await _userManager.FindByNameAsync(externalLoginInfo.Principal.FindFirst(ClaimTypes.Name).Value);
                if (user==null)
                {
                    user = await _userManager.FindByEmailAsync(externalLoginInfo.Principal.FindFirst(ClaimTypes.Email).Value);
                }
                if (user==null)
                {
                    user = new AppUser
                    {
                        Email = externalLoginInfo.Principal.FindFirst(ClaimTypes.Email).Value,
                        UserName = MetinDuzenleyici.MetinDuzelt(externalLoginInfo.Principal.FindFirst(ClaimTypes.Name).Value)
                    };
                    IdentityResult result = await _userManager.CreateAsync(user);
                    if (result.Succeeded)
                    {
                        await _userManager.AddLoginAsync(user, externalLoginInfo);
                    }

                }
                await _signInManager.SignInAsync(user, false);
                return true;
            }
        }

        public async Task<(bool, bool)> SignInAsync(AppUserLoginVM model)
        {
            AppUser user = await _userManager.FindByNameAsync(model.UsernameorEmail);
            Microsoft.AspNetCore.Identity.SignInResult result = null;
            if (user==null)
            {
                user = await _userManager.FindByEmailAsync(model.UsernameorEmail);
            }
            if (user!=null)
            {
                result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            }
            return result != null ? (result.Succeeded, result.RequiresTwoFactor) : (false, false);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
