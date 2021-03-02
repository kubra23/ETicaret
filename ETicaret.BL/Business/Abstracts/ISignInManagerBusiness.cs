using ETicaret.Entities.Authentications.ViewModels.AppUserVM;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.BL.Business.Abstracts
{
    public interface ISignInManagerBusiness
    {
        Task<(bool,bool)> SignInAsync(AppUserLoginVM model);
        Task SignOutAsync();
        AuthenticationProperties ExternalLogin(string providerName, string redirectUrl);
        Task<bool> ExternalResponse();
    }
}
