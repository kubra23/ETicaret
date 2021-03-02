using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETicaret.BL.Business.Abstracts;
using ETicaret.Entities;
using ETicaret.Entities.ViewModels.UrunVM;
using ETicaret.Entities.ViewModels.YorumVM;
using ETicaret.PL.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.PL.Controllers
{
    public class UrunController : BaseController
    {
        IKategoriBusiness _kategoriBusiness;
        IUrunBusiness _urunBusiness;
        public UrunController(IKategoriBusiness kategoriBusiness, IUrunBusiness urunBusiness)
        {
            _kategoriBusiness = kategoriBusiness;
            _urunBusiness = urunBusiness;
        }
        public IActionResult Index(int? kategoriId)
        {
            ViewBag.Kategoriler = _kategoriBusiness.Listele();
            List<UrunAnasayfaListeleVM> urunler = null;
            if (kategoriId!=null)
            {
                urunler = _urunBusiness.AnasayfaKategoriyeUygunUrunleriListele(kategoriId.Value);
                ViewBag.KategoriAdi = _kategoriBusiness.KategoriGetir(kategoriId.Value).KategoriAdi + "Ürünleri";

            }
            else
            {
                urunler = _urunBusiness.AnasayfaTumUrunleriListele();
                ViewBag.KategoriAdi = "Ürünler";
            }
            return View(urunler);
        }

        public IActionResult Detay(int id)
        {
            ViewBag.Kategoriler = _kategoriBusiness.Listele();
            Urun urun = _urunBusiness.UrunGetir(id);
            return View(urun);
        }
        [HttpPost]
        public IActionResult YorumEkle(int id, YorumEkleVM model)
        {
            model.UrunId = id;
            bool sonuc = _urunBusiness.UruneYorumEkle(model);
            MesajYaz(sonuc, "Yorum başarıyla eklenmiştir.Moderatörler tarafından incelendikten sonra yayınlanacaktır.", "Yorum eklenirken beklenmeyen bir hatayla karşılaşılmıştır.");
            return RedirectToAction("Detay", "Urun", new { id = id });
        }
    }
}