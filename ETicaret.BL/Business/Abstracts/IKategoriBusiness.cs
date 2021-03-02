using ETicaret.Entities;
using ETicaret.Entities.ViewModels.KategoriVM;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaret.BL.Business.Abstracts
{
   public interface IKategoriBusiness
    {
       Kategori Ekle(KategoriEkleVM model);
        List<KategoriListeleVM> Listele();
        (bool, Kategori) Sil(int id);
        KategoriDuzenleVM DuzenlenecekKategoriyiGetir(int id);
        (bool, Kategori) Guncelle(KategoriDuzenleVM model);
        Kategori KategoriGetir(int kategoriId);
    }
}
