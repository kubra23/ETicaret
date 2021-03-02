using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ETicaret.Entities;
using Microsoft.AspNetCore.Http;

namespace ETicaret.BL.Business.Abstracts
{
    public interface IResimBusiness
    {
        Task<(bool, List<string>)> Upload(List<IFormFile> files, string path, string dosyaAdi);
        bool Ekle(List<string> resimler, int urunId);
        bool ResimleriFizikselSil(List<string> resimler, string path);
        bool ResimSil(int resimId);
        Resim ResimGetir(int resimId);
    }
}
