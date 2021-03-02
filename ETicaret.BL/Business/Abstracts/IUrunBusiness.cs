using ETicaret.Entities;
using ETicaret.Entities.ViewModels.UrunVM;
using ETicaret.Entities.ViewModels.YorumVM;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaret.BL.Business.Abstracts
{
   public interface IUrunBusiness
    {
        (bool, int) Ekle(UrunEkleDuzenleVM model, List<string> resimler);
        List<UrunListeleVM> Listele();
        List<UrunAnasayfaListeleVM> AnasayfaTumUrunleriListele();
        Urun Sil(int id, string path);
        UrunResimListeleVM UrunResimListele(int id);
        bool VitrinResmiDegistir(int urunId, int resimId);
        Urun UrunGetir(int id);
        UrunEkleDuzenleVM UrunEkleDuzenleNesnesiGetir(int id);
        Urun Guncelle(UrunEkleDuzenleVM model, int urunId, List<string> resimler);
        bool UrunResimleriSil(int urunId, string path);
        List<UrunAnasayfaListeleVM> AnasayfaKategoriyeUygunUrunleriListele(int kategoriId);
        bool UruneYorumEkle(YorumEkleVM model);
    }
}
