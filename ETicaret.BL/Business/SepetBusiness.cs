using ETicaret.BL.Business.Abstracts;
using ETicaret.BL.Repositories.Abstracts;
using ETicaret.BL.Repositories.Concretes;
using ETicaret.Entities;
using ETicaret.Entities.Authentications;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.BL.Business
{
    public class SepetBusiness : ISepetBusiness
    {
        UserManager<AppUser> _userManager;
        ISepetUrunRepository _sepetUrunRepository;
        public SepetBusiness(UserManager<AppUser>userManager)
        {
            _userManager = userManager;
            _sepetUrunRepository = new SepetUrunRepository();

        }
        public async Task<Sepet> KullaniciSepetGetir(string userName)
        {
            AppUser appUser = await _userManager.Users.Include(u => u.Sepet).ThenInclude(s => s.Urunler).ThenInclude(u => u.Urun).FirstOrDefaultAsync(u => u.UserName == userName);
            return appUser.Sepet;
        }

        public async Task<SepetUrun> UrunEkle(string userName, int urunId)
        {
            AppUser appUser = await _userManager.Users.Include(u => u.Sepet).ThenInclude(s => s.Urunler).ThenInclude(u => u.Urun).FirstOrDefaultAsync(u => u.UserName == userName);
            SepetUrun sepetUrun = appUser.Sepet.Urunler.FirstOrDefault(u => u.UrunId == urunId);
            if (appUser.Sepet.Urunler.Count>0 && sepetUrun !=null)
            {
                sepetUrun.Adet++;
            }
            else
            {
                sepetUrun = new SepetUrun()
                {
                    SepetId = appUser.Sepet.Id,
                    Adet = 1,
                    UrunId = urunId
                };
                _sepetUrunRepository.Add(sepetUrun);
                _sepetUrunRepository.Save();

                sepetUrun = _sepetUrunRepository.GetTable().Include(s => s.Urun).FirstOrDefault(s => s.UrunId == urunId && s.SepetId == appUser.Sepet.Id);
            }
            await _userManager.UpdateAsync(appUser);
            await _userManager.UpdateSecurityStampAsync(appUser);
            return sepetUrun;

        }
        public async Task<decimal> Sil(string userName, int urunId)
        {
            AppUser appUser = await _userManager.Users.Include(u => u.Sepet).ThenInclude(s => s.Urunler).ThenInclude(u => u.Urun).FirstOrDefaultAsync(u => u.UserName == userName);
            SepetUrun silinecekUrun = appUser.Sepet.Urunler.FirstOrDefault(u => u.UrunId == urunId);
            decimal toplamTutar = appUser.Sepet.Urunler.Sum(u => u.Adet * u.Urun.Fiyat);
            appUser.Sepet.Urunler.Remove(silinecekUrun);
            await _userManager.UpdateAsync(appUser);
            await _userManager.UpdateSecurityStampAsync(appUser);
            return toplamTutar;
        }

        public async Task<decimal> AdetDegistir(string userName, int urunId, int adet)
        {
            AppUser appUser = _userManager.Users.Include(u => u.Sepet).ThenInclude(s => s.Urunler).ThenInclude(u => u.Urun).FirstOrDefault(u => u.UserName == userName);
            SepetUrun sepetUrun = appUser.Sepet.Urunler.FirstOrDefault(u => u.UrunId == urunId);
            sepetUrun.Adet = adet;
            decimal toplamTutar = appUser.Sepet.Urunler.Sum(u => u.Adet * u.Urun.Fiyat);
            IdentityResult result = await _userManager.UpdateSecurityStampAsync(appUser);
            await _userManager.UpdateSecurityStampAsync(appUser);
            return toplamTutar;
        }
    }
}
