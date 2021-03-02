using ETicaret.BL.Business.Abstracts;
using ETicaret.BL.Repositories.Abstracts;
using ETicaret.BL.Repositories.Concretes;
using ETicaret.Entities;
using ETicaret.Entities.ViewModels.KategoriVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETicaret.BL.Business
{
    public class KategoriBusiness : IKategoriBusiness
    {
        IKategoryRepository _kategoryRepository;
        IUrunBusiness _urunBusiness;
        public KategoriBusiness()
        {
            _kategoryRepository = new KategoriRepository();
            _urunBusiness = new UrunBusiness();
        }
        public KategoriDuzenleVM DuzenlenecekKategoriyiGetir(int id)
        {
            Kategori duzenlenecekKategori = _kategoryRepository.GetSingle(k => k.Id == id);
            KategoriDuzenleVM kategoriDuzenleVM = new KategoriDuzenleVM
            {
                Id = duzenlenecekKategori.Id,
                KategoriAdi = duzenlenecekKategori.KategoriAdi
            };
            return kategoriDuzenleVM;
        }

        public Kategori Ekle(KategoriEkleVM model)
        {
            return _kategoryRepository.Add(new Kategori
            {
                KategoriAdi = model.KategoriAdi
            });
        }

        public (bool, Kategori) Guncelle(KategoriDuzenleVM model)
        {
            Kategori guncellenecekKategori = _kategoryRepository.GetSingle(k => k.Id == model.Id);
            string guncellenmemisAdi = guncellenecekKategori.KategoriAdi;
            guncellenecekKategori.KategoriAdi = model.KategoriAdi;
            var sonuc = _kategoryRepository.Update(guncellenecekKategori);
            guncellenecekKategori.KategoriAdi = guncellenmemisAdi;
            return (sonuc, guncellenecekKategori);
        }

        public Kategori KategoriGetir(int kategoriId)
        {
            return _kategoryRepository.GetSingle(k => k.Id == kategoriId);
        }

        public List<KategoriListeleVM> Listele()
        {
            return _kategoryRepository.Get().Select(k => new KategoriListeleVM
            {
                Id = k.Id,
                KategoriAdi = k.KategoriAdi,
                UrunAdet = _urunBusiness.AnasayfaKategoriyeUygunUrunleriListele(k.Id).Count
            }).ToList();
        }

        public (bool, Kategori) Sil(int id)
        {
            Kategori silinecekKategori = _kategoryRepository.GetSingle(k => k.Id == id);
            return (_kategoryRepository.Remove(silinecekKategori), silinecekKategori);
        }
    }
}
