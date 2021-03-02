using ETicaret.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.BL.Business.Abstracts
{
    public interface ISepetBusiness
    {
        Task<Sepet> KullaniciSepetGetir(string userName);
        Task<SepetUrun> UrunEkle(string userName, int urunId);
        Task<decimal> Sil(string userName, int urunId);
        Task<decimal> AdetDegistir(string userName, int urunId, int adet);
    }
}
