using System.Linq;
using ETicaret.Entities.ViewModels.KategoriVM;
using Microsoft.AspNetCore.Mvc;
using ETicaret.BL.Repositories.Abstracts;
using ETicaret.Entities;
using ETicaret.BL.Business.Abstracts;
using ETicaret.PL.Areas.yonetimPaneli.Controllers.Base;
using Microsoft.AspNetCore.Authorization;

namespace ETicaret.PL.Areas.yonetimPaneli.Controllers
{
    [Area ("yonetimPaneli")]
    [Authorize()]
    public class KategoriController : BaseController
    {
        IKategoriBusiness _kategoriBusiness;
        public KategoriController(IKategoriBusiness kategoriBusiness)
        {
            _kategoriBusiness = kategoriBusiness;
        }
        [Authorize(Roles = "Administrator,Moderator,Editor")]
        public IActionResult Index()
        {
            var kategoriler = _kategoriBusiness.Listele();
            return View(kategoriler);
        }
        [Authorize(Roles = "Administrator,Moderator")]
        public IActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Administrator,Moderator")]
        public IActionResult Ekle(KategoriEkleVM model)
        {
            if (ModelState.IsValid)
            {
                _kategoriBusiness.Ekle(model);
                return RedirectToAction("Index");
            }
            else
                return View(model);
        }
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Sil(int id)
        {
            var (sonuc, kategori) = _kategoriBusiness.Sil(id);
            
            MesajYaz(sonuc, kategori.KategoriAdi + "isimli kategoriyi silme işlemi başarıyla gerçekleştirilmiştir.", kategori.KategoriAdi + "isimli kategoriyi silme işlemi yapılırken beklenmeyen bir hatayla karşılaşıldı.");
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Moderator")]
        public IActionResult Duzenle(int id) 
        {
            KategoriDuzenleVM guncellenecekKategori = _kategoriBusiness.DuzenlenecekKategoriyiGetir(id);
            return View(guncellenecekKategori);
        }
        [HttpPost]
        [Authorize(Roles = "Moderator")]
        public IActionResult Duzenle(KategoriDuzenleVM model)
        {

            if (!ModelState.IsValid)
                return View(model);

            var (sonuc, kategori) = _kategoriBusiness.Guncelle(model);
            MesajYaz(sonuc, kategori.KategoriAdi + "isimli kategori" + model.KategoriAdi + "ismiyle başarıyla değiştirilmiştir.", kategori.KategoriAdi + "isimli kategori" + model.KategoriAdi + "ismiyle değiştirilirken beklenmeyen bir hatayla karşılaşıldı.");

            return RedirectToAction("Index");
        }
    }
}