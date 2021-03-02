using ETicaret.BL.Business.Abstracts;
using ETicaret.BL.Repositories.Abstracts;
using ETicaret.BL.Repositories.Concretes;
using ETicaret.Entities;
using ETicaret.Entities.ViewModels.ResimVM;
using ETicaret.Entities.ViewModels.UrunVM;
using ETicaret.Entities.ViewModels.YorumVM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ETicaret.BL.Business
{
    public class UrunBusiness : IUrunBusiness
    {
        IUrunRepository _urunRepository;
        IResimRepository _resimRepository;
        IResimBusiness _resimBusiness;
        public UrunBusiness()
        {
            _urunRepository = new UrunRepository();
            _resimRepository = new ResimRepository();
            _resimBusiness = new ResimBusiness();

        }

        public List<UrunAnasayfaListeleVM> AnasayfaTumUrunleriListele()
        {
            return _urunRepository.GetTable().Include(u => u.Marka).Include(u => u.Kategori).ToList().Select(u =>
            {
                Resim resim = _resimBusiness.ResimGetir(u.VitrinResmiId);
                return new UrunAnasayfaListeleVM
                {
                    Adi = u.UrunAdi,
                    Fiyat = u.Fiyat,
                    MarkaAdi = u.Marka.MarkaAdi,
                    KategoriAdi = u.Kategori.KategoriAdi,
                    Id = u.Id,
                    VitrinResmi = resim != null ? resim.ResimAdi : ""
                };
            }).ToList();
        }

        public (bool, int) Ekle(UrunEkleDuzenleVM model, List<string> resimler)
        {
            List<Resim> eklenecekResimler = new List<Resim>();
            foreach (var resim in resimler)
            {
                eklenecekResimler.Add(new Resim
                {
                    ResimAdi = resim,
                });
            }
            _urunRepository.Add(new Urun
            {
                UrunAdi = model.Adi,
                Fiyat = model.Fiyat.HasValue ? model.Fiyat.Value : 0,
                KategoriId = model.KategoriId,
                MarkaId = model.MarkaId,
                StokAdet = model.StokAdet.HasValue ? model.StokAdet.Value : 0,
                UretimTarihi = model.UretimTarihi.HasValue ? model.UretimTarihi.Value : DateTime.Now,
                Resimler = eklenecekResimler

            });
            var sonEklenenUrun = _urunRepository.GetLast<int>(u => u.Id);
            Urun resimliUrun = _urunRepository.GetTable().Include(u => u.Resimler).FirstOrDefault(u => u.Id == sonEklenenUrun.Id);
            resimliUrun.VitrinResmiId = resimliUrun.Resimler.ToList()[0].Id;
            _urunRepository.Update(resimliUrun);

            return (true, sonEklenenUrun.Id);
        }

        public Urun Guncelle(UrunEkleDuzenleVM model, int urunId, List<string> resimler)
        {
            List<Resim> eklenecekResimler = new List<Resim>();
            foreach (var resim in resimler)
            {
                eklenecekResimler.Add(new Resim
                {
                    ResimAdi = resim
                });

            }
            Urun urun = _urunRepository.GetSingle(u => u.Id == urunId);
            urun.UrunAdi = model.Adi;
            urun.Fiyat = model.Fiyat.Value;
            urun.KategoriId = model.KategoriId;
            urun.MarkaId = model.MarkaId;
            urun.StokAdet = model.StokAdet.Value;
            urun.UretimTarihi = model.UretimTarihi.Value;
            urun.Resimler = eklenecekResimler;
            _urunRepository.Update(urun);

            Urun resimliUrun = _urunRepository.GetTable().Include(u => u.Resimler).FirstOrDefault(u => u.Id == urunId);
            resimliUrun.VitrinResmiId = resimliUrun.Resimler.ToList()[0].Id;
            _urunRepository.Update(resimliUrun);
            return urun;

        }

        public List<UrunAnasayfaListeleVM> AnasayfaKategoriyeUygunUrunleriListele(int kategoriId)
        {
            return _urunRepository.GetTable().Include(u => u.Marka).Include(u => u.Kategori).Where(u => u.KategoriId == kategoriId).ToList().Select(u =>
            {
                Resim resim = _resimBusiness.ResimGetir(u.VitrinResmiId);
                return new UrunAnasayfaListeleVM
                {
                    Adi = u.UrunAdi,
                    Fiyat = u.Fiyat,
                    KategoriAdi = u.Kategori.KategoriAdi,
                    MarkaAdi = u.Marka.MarkaAdi,
                    Id = u.Id,
                    VitrinResmi = resim.ResimAdi
                };
            }).ToList();
        }

        public Urun Sil(int id, string path)
        {
            Urun silinecekUrun = _urunRepository.GetTable().Include(u => u.Resimler).FirstOrDefault(u => u.Id == id);
            //foreach (var resim in silinecekUrun.Resimler)
            //{
            //    File.Delete(path + "\\dosyalar\\resimler\\" + resim.ResimAdi);

            //}
            _resimBusiness.ResimleriFizikselSil(silinecekUrun.Resimler.Select(r => r.ResimAdi).ToList(), path);
            bool sonuc = _urunRepository.Remove(silinecekUrun);
            return silinecekUrun;

        }

        public UrunEkleDuzenleVM UrunEkleDuzenleNesnesiGetir(int id)
        {
            Urun urun = UrunGetir(id);
            return new UrunEkleDuzenleVM
            {
                Adi = urun.UrunAdi,
                Fiyat = urun.Fiyat,
                KategoriId = urun.KategoriId,
                MarkaId = urun.MarkaId,
                StokAdet = urun.StokAdet,
                UretimTarihi = urun.UretimTarihi
            };
        }

        public Urun UrunGetir(int id)
        {
            return _urunRepository.GetTable().Include(u => u.Resimler).Include(u => u.Marka).Include(u => u.Kategori).Include(u=>u.Yorumlar).FirstOrDefault(u => u.Id == id);
        }

        public bool UrunResimleriSil(int urunId, string path)
        {
            try
            {
                List<Resim> urunResimler = _resimRepository.GetWhere(r => r.UrunId == urunId);
                _resimBusiness.ResimleriFizikselSil(urunResimler.Select(r => r.ResimAdi).ToList(), path);
                foreach (var resim in urunResimler)
                {
                    _resimBusiness.ResimSil(resim.Id);
                }
                return true;

            }
            catch
            {

                return false;
            }
        }

        public UrunResimListeleVM UrunResimListele(int id)
        {
            Urun urun = _urunRepository.GetTable().Include(u => u.Resimler).FirstOrDefault(u => u.Id == id);

            return new UrunResimListeleVM
            {
                Resimler = urun.Resimler.Select(r => new ResimListeVM
                {
                    Id=r.Id,
                    ResimAdi=r.ResimAdi,
                    Vitrin=r.Id== urun.VitrinResmiId ? true :false
                }).ToList(),
                Adi = urun.UrunAdi
            };
            
        }

        public bool VitrinResmiDegistir(int urunId, int resimId)
        {
            Urun urun = _urunRepository.GetSingle(u => u.Id == urunId);
            urun.VitrinResmiId = resimId;
            _urunRepository.Update(urun);
            return true;
        }


        public List<UrunListeleVM> Listele()
        {
            return _urunRepository.GetTable().Include(u => u.Kategori).Include(u => u.Marka).Select(u => new UrunListeleVM
            {
                Id = u.Id,
                UretimTarihi = u.UretimTarihi,
                Adi = u.UrunAdi,
                Fiyat = u.Fiyat,
                KategoriAdi = u.Kategori.KategoriAdi,
                MarkaAdi = u.Marka.MarkaAdi,
                StokAdet = u.StokAdet
            }).ToList();
        }
        public bool UruneYorumEkle(YorumEkleVM model)
        {
            Urun urun = _urunRepository.GetSingle(u => u.Id == model.UrunId);
            urun.Yorumlar = new List<Yorum>
            {
                new Yorum
                {
                    Adi = model.Adi,
                    Soyadi = model.Soyadi,
                    Onay = false,
                    Email = model.Mail,
                    Mesaj=model.Mesaj
                }
            };
            return _urunRepository.Save() > 0;
        }

    }
}
