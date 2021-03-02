using ETicaret.Entities.Authentications;
using ETicaret.Entities.Authentications.ViewModels.AppRoleVM;
using ETicaret.Entities.Authentications.ViewModels.AppUserVM;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.BL.Business.Abstracts
{
    public interface IUserManagerBusiness
    {
        Task<bool> CreateAsync(AppUserKayitOlVM model);
        List<AppUserListeleVM> AllUser();
        Task<(bool, string)> DeleteAsync(string id);
        Task<AppUser> KullaniciGetir(int id);
        Task<AppUser> KullaniciGetir(string emailorName);
        Task<AppUserGuncelleVM> GuncellenecekKullaniciGetir(string
             emailorName);
        Task<(bool, bool)> MailGonder(string userEmail);
        Task<bool> SifreGuncelle(int userId, string token, AppUserSifreGuncelleVM model);
        Task<List<AppRoleAtaVM>> TumRolleriGetir(int userId);
        Task<(bool, string)> RolAtaveSil(int userId, List<AppRoleAtaVM> atanmisRoller);
        Task<bool> AddLoginTimeClaim(AppUserLoginVM model);
        Task<bool> KullaniciGuncelle(AppUserGuncelleVM model);
    }
}
