using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETicaret.BL.Business;
using ETicaret.BL.Business.Abstracts;
using ETicaret.Entities;
using ETicaret.Entities.Authentications;
using ETicaret.Entities.Authentications.ViewModels.AppUserVM;
using ETicaret.Entities.ViewModels.UrunVM;
using ETicaret.PL.Areas.yonetimPaneli.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.PL.Areas.yonetimPaneli.Controllers
{
    [Area("yonetimPaneli")]
    [Authorize(Roles = "Administrator,Moderator,Editor")]
    public class UrunController : BaseController
    {
        IKategoriBusiness _kategoriBusiness;
        IMarkaBusiness _markaBusiness;
        IUrunBusiness _urunBusiness;
        IWebHostEnvironment _webHostEnvironment;
        IResimBusiness _resimBusiness;
        public UrunController(IKategoriBusiness kategoriBusiness,
            IMarkaBusiness markaBusiness,
            IUrunBusiness urunBusiness,
            IWebHostEnvironment webHostEnvironment,
            IResimBusiness resimBusiness)
        {
            _kategoriBusiness = kategoriBusiness;
            _markaBusiness = markaBusiness;
            _urunBusiness = urunBusiness;
            _webHostEnvironment = webHostEnvironment;
            _resimBusiness = resimBusiness;
        }
        public IActionResult Index()
        {
            return View(_urunBusiness.Listele());
        }
        //[Authorize(Roles = "Administrator")]
        public IActionResult Ekle(int? id)
        {
            ViewBag.Kategoriler = _kategoriBusiness.Listele();
            ViewBag.Markalar = _markaBusiness.Listele();
            if (id != null)
            {
                //Düzenleme
                //View'e düzenlenecek modelı göndereceğiz.
                UrunEkleDuzenleVM duzenlenecekUrun = _urunBusiness.UrunEkleDuzenleNesnesiGetir(id.Value);
                return View(duzenlenecekUrun);
            }
            //Ekleme
            //View'e model göndermeyeceğiz.
            return View();
        }
        [HttpPost]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Ekle(int? id, UrunEkleDuzenleVM model, List<IFormFile> files)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Kategoriler = _kategoriBusiness.Listele();
                ViewBag.Markalar = _markaBusiness.Listele();
                return View(model);
            }

            if (id != null)
            {
                //Düzenleme yapılacak
                _urunBusiness.UrunResimleriSil(id.Value, _webHostEnvironment.WebRootPath);
            }
            var (sonuc1, resimler) = await _resimBusiness.Upload(files, _webHostEnvironment.WebRootPath, model.Adi);
            if (id != null)
            {
                _urunBusiness.Guncelle(model, id.Value, resimler);
                MesajYaz(true, model.Adi + " isimli ürün başarıyla güncellenmiştir.", model.Adi + "isimli ürün güncellenirken beklenmeyen bir hatayla karşılaşılmıştır.");
            }
            else
            {
                _urunBusiness.Ekle(model, resimler);
                MesajYaz(true, model.Adi + " isimli ürün başarıyla eklenmiştir.", model.Adi + "isimli ürün eklenirken beklenmeyen bir hatayla karşılaşılmıştır.");
            }
            return RedirectToAction("Index");
        }
        //[Authorize(Policy = "TimeControl")]
        //[Authorize(Roles = "Administrator")]
        public IActionResult Sil(int id)
        {
            Urun silinenUrun = _urunBusiness.Sil(id, _webHostEnvironment.WebRootPath);

            MesajYaz(silinenUrun != null, silinenUrun.UrunAdi + " isimli ürün başarıyla silinmiştir. " + silinenUrun.Resimler.Count + " adet resim silinmiştir.", silinenUrun.UrunAdi + " isimli ürün silinirken beklenmeyen bir hatayla karşılaşıldı.");
            return RedirectToAction("Index");
        }

        public JsonResult UrunBilgileriDondur(int id)
        {
            return Json(_urunBusiness.UrunResimListele(id));
        }
        //[Authorize(Roles = "Administrator")]
        public JsonResult UrunVitrinDegistir(int urunId, int resimId)
        {
            return Json(_urunBusiness.VitrinResmiDegistir(urunId, resimId) ? "Vitrin başarıyla değiştirilmiştir" : "Vitrin değiştirilirken beklenmeyen bir hatayla karşılaşıldı.");
        }

    }
}