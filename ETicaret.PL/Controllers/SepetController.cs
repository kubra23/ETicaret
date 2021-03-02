using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETicaret.BL.Business.Abstracts;
using ETicaret.Entities;
using ETicaret.PL.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.PL.Controllers
{
    public class SepetController :BaseController
    {
        ISepetBusiness _sepetBusiness;
        public SepetController(ISepetBusiness sepetBusiness)
        {
            _sepetBusiness = sepetBusiness;
        }
        [Authorize]
        public async Task<JsonResult> SepetGetir()
        {
            Sepet sepet = await _sepetBusiness.KullaniciSepetGetir(User.Identity.Name);
            return Json(new
            {
                Urunler = sepet.Urunler.Select(u => new
                {
                    u.Adet,
                    Adi=u.Urun.UrunAdi,
                    u.Urun.Fiyat,
                    UrunId=u.Urun.Id,
                    u.Urun.StokAdet
                })

            });
        }
          [Authorize]
          public async Task<JsonResult>UrunEkle(int id)
        {
            SepetUrun sepetUrun = await _sepetBusiness.UrunEkle(User.Identity.Name, id);
            return Json(new
            {
                sepetUrun.Adet,
                sepetUrun.Urun.UrunAdi,
                sepetUrun.Urun.Fiyat,
                sepetUrun.Urun.Id,
                sepetUrun.Urun.StokAdet
            });
        }
        [Authorize]
        public async Task<JsonResult> Sil(int id)
        {
            return Json(await _sepetBusiness.Sil(User.Identity.Name,id));

           }
        [Authorize]
        public async Task<JsonResult>AdetDegistir(int id,int adet)
        {
            return Json(await _sepetBusiness.AdetDegistir(User.Identity.Name, id, adet));
        }
    }
}