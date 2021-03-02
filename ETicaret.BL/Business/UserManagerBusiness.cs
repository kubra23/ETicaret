using ETicaret.BL.Business.Abstracts;
using ETicaret.BL.Services;
using ETicaret.Entities;
using ETicaret.Entities.Authentications;
using ETicaret.Entities.Authentications.ViewModels.AppRoleVM;
using ETicaret.Entities.Authentications.ViewModels.AppUserVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.BL.Business
{
    public class UserManagerBusiness : IUserManagerBusiness
    {
        UserManager<AppUser> _userManager;
        RoleManager<AppRole> _roleManager;
        public UserManagerBusiness(UserManager<AppUser> userManager,
            RoleManager<AppRole>roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> AddLoginTimeClaim(AppUserLoginVM model)
        {
            AppUser user = null;
            user = await _userManager.FindByNameAsync(model.UsernameorEmail);
            if (user==null)
            {
                user = await _userManager.FindByEmailAsync(model.UsernameorEmail);
            }
            if (user != null)
            {
                Claim loginTimeClaim = (await _userManager.GetClaimsAsync(user)).FirstOrDefault(c => c.Type == "loginTime");
                if (loginTimeClaim != null)
                {
                    await _userManager.RemoveClaimAsync(user, loginTimeClaim);

                }
                IdentityResult result = await _userManager.AddClaimAsync(user, new Claim("loginTime", DateTime.UtcNow.ToString()));
                return result.Succeeded;
            }
            return false;
        }

        public List<AppUserListeleVM> AllUser()
        {
            return _userManager.Users.ToList().Select(u => new AppUserListeleVM
            {
                Id = u.Id,
                Email = u.Email,
                Username = u.UserName,
                Roller=(List<string>)_userManager.GetRolesAsync(u).Result
            }).ToList();
        }

        public async Task<bool> CreateAsync(AppUserKayitOlVM model)
        {
            IdentityResult result = await _userManager.CreateAsync(new AppUser
            {
                UserName = model.UserName,
                Email = model.Email,
                Sepet = new Sepet() { }
            }, model.Password);
            return result.Succeeded;
        }

        public async Task<(bool, string)> DeleteAsync(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            IdentityResult result = await _userManager.DeleteAsync(user);
            return (result.Succeeded, user.UserName);
        }


        public async Task<AppUser> KullaniciGetir(int id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<AppUser> KullaniciGetir(string emailorName)
        {
            AppUser user= await _userManager.FindByEmailAsync(emailorName);
            if (user==null)
            {
                user = await _userManager.FindByNameAsync(emailorName);
            }
            return user;

        }
        public async Task<(bool, bool)> MailGonder(string userEmail)
        {
            AppUser user = await KullaniciGetir(userEmail);
            if (user != null)
            {
                string token = await _userManager.GeneratePasswordResetTokenAsync(user);

                byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(token);
                string codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);

                string body = "Sayın " + user.UserName + ",<br><br>";
                body += "Şifrenizi yenileme isteğiniz üzerine bu e-posta gönderilmiştir.<br><br><br><br>";
                body += "Şifrenizi güncellemek için lütfen aşağıdaki adrese tıklayınız.";
                body += "<a target=\"_blank\" href=\"https://localhost:5001/User/SifreGuncelle?userId=" + user.Id + "&token=" + codeEncoded + "\">";
                body += "Yeni şifre talebi için tıklayınız</a>";

                bool mesajSonuc = MailService.Send(user.Email, "Şifre Yenileme Talebi", body);
                return (true, mesajSonuc);
            }
            return (false, false);
        }
        public async Task<(bool, string)> RolAtaveSil(int userId, List<AppRoleAtaVM> atanmisRoller)
        {
            AppUser appUser = await KullaniciGetir(userId);
            try
            {
                foreach (var rol in atanmisRoller)
                {
                    if (rol.HasAssign)
                        await _userManager.AddToRoleAsync(appUser, rol.Name);
                    else
                        await _userManager.RemoveFromRoleAsync(appUser, rol.Name);
                }
                return (true, appUser.UserName);

            }
            catch
            {
                return (false, appUser.UserName);

            }
        }
        public async Task<bool> SifreGuncelle(int userId, string token, AppUserSifreGuncelleVM model)
        {
            byte[] codeDecodeBytes = WebEncoders.Base64UrlDecode(token);
            string codeDecoded = Encoding.UTF8.GetString(codeDecodeBytes);
            AppUser appUser = await KullaniciGetir(userId);
            IdentityResult result = await _userManager.ResetPasswordAsync(appUser, codeDecoded, model.Sifre);
            if (result.Succeeded)
            {
                await _userManager.UpdateSecurityStampAsync(appUser);
            }
            return result.Succeeded;
        }
        public async Task<List<AppRoleAtaVM>> TumRolleriGetir(int userId)
        {
            List<AppRoleAtaVM> atanmisRoller = new List<AppRoleAtaVM>();
            AppUser appUser = await KullaniciGetir(userId);
            List<string> roller = (List<string>)await _userManager.GetRolesAsync(appUser);
            List<AppRole> tumRoller = _roleManager.Roles.ToList();
            foreach (var rol in tumRoller)
            {
                atanmisRoller.Add(new AppRoleAtaVM
                {
                    Id = rol.Id,
                    Name = rol.Name,
                    HasAssign = roller.Contains(rol.Name)
                });

            }
            return atanmisRoller;
        }
        public async Task<AppUserGuncelleVM> GuncellenecekKullaniciGetir(string emailorName)
        {
            AppUser appUser = await KullaniciGetir(emailorName);

            return new AppUserGuncelleVM
            {
                Email = appUser.Email,
                PhoneNumber = appUser.PhoneNumber,
                TuttuguTakim = appUser.TuttuguTakim,
                Username = appUser.UserName,
                TwoFactorType=appUser.TwoFactorType,
                TwoFactorEnabled=appUser.TwoFactorEnabled
            };
        }

        public async Task<bool> KullaniciGuncelle(AppUserGuncelleVM model)
        {
            AppUser user = await KullaniciGetir(model.Username);
            if (user==null)
                user = await KullaniciGetir(model.Email);
            user.PhoneNumber = model.PhoneNumber;
            user.TuttuguTakim = model.TuttuguTakim;
            await _userManager.UpdateAsync(user);
            if (!string.IsNullOrEmpty(model.Password) && !string.IsNullOrEmpty(model.PasswordTekrar) && !string.IsNullOrEmpty(model.CurrentPassword))
            {
                IdentityResult result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.Password);
                if (!result.Succeeded)
                {
                    return false;
                }
                else
                {

                }
            }
            return true;
        }




    }
}
