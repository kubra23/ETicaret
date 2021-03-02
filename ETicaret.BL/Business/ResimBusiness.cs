using ETicaret.BL.Business.Abstracts;
using ETicaret.BL.Repositories.Abstracts;
using ETicaret.BL.Repositories.Concretes;
using ETicaret.BL.Services;
using ETicaret.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.BL.Business
{
    public class ResimBusiness : IResimBusiness
    {
        IResimRepository _resimRepository;
        public ResimBusiness()
        {
            _resimRepository = new ResimRepository();
        }
        string OnayliDosyaAdi(string path,string dosyaAdi)
        {
            //Eğer bu isimde bir dosya iligli dizinde varsa true, yoksa false dönecek.
            FileInfo file = new FileInfo(path + "\\" + dosyaAdi);
            if (file.Exists)
            {
                string uzantisizDosyaAdi = Path.GetFileNameWithoutExtension(dosyaAdi);
                string uzanti = Path.GetExtension(dosyaAdi);

                int index = uzantisizDosyaAdi.Length - 1;
                while (index>=0)
                {
                    char karakter = uzantisizDosyaAdi[index];
                    if (karakter == '-'|| index ==0 )
                    {
                        string sayisalIfade = uzantisizDosyaAdi.Substring(index + 1, uzantisizDosyaAdi.Length - index - 1);
                        if (int.TryParse(sayisalIfade,out int sayi))
                        {
                            uzantisizDosyaAdi = uzantisizDosyaAdi.Remove(index, uzantisizDosyaAdi.Length - index) + "-"+(++sayi);
                        }
                        else
                        {
                            uzantisizDosyaAdi += "-" + 2;
                        }
                        break;

                    }
                    index--;

                }
                return OnayliDosyaAdi(path, uzantisizDosyaAdi + uzanti);

            }
            return dosyaAdi;
        }
        string DosyaIsmiDuzelt(string dosyaAdi, string uzanti)
        {
            return MetinDuzenleyici.MetinDuzelt(dosyaAdi) + uzanti;
        }
        public async Task<(bool, List<string>)> Upload(List<IFormFile> files, string path, string dosyaAdi)
        {
            string uploadPath = path + "\\dosyalar\\resimler";
            List<string> resimler = new List<string>();
            try
            {
                foreach (IFormFile file in files)
                {
                    string yeniIsim = DosyaIsmiDuzelt(dosyaAdi, Path.GetExtension(file.FileName));
                    string onayliDosyaAdi = OnayliDosyaAdi(uploadPath, yeniIsim);
                    resimler.Add(onayliDosyaAdi);
                    FileStream fileStream = new FileStream(uploadPath + "\\" + onayliDosyaAdi, FileMode.Create);
                    await file.CopyToAsync(fileStream);
                    fileStream.Close();
                    fileStream.Dispose();

                }
                return (true, resimler);

            }
            catch
            {

                return (false, resimler);
            }
        }
        public bool Ekle(List<string> resimler, int urunId)
        {
            try
            {
                foreach (var resim in resimler)
                {
                   Resim ressimObj = new Resim()
                    {
                        ResimAdi = resim,
                        UrunId = urunId,

                    };
                    _resimRepository.Add(ressimObj);

                }
                return true;

            }
            catch
            {
                return false;
            }
        }

        public bool ResimleriFizikselSil(List<string> resimler, string path)
        {
            try
            {
                foreach (var resim in resimler)
                {
                    File.Delete(path + "\\dosyalar\\resimler\\" + resim);

                }
                return true;

            }
            catch
            {

                return false;
            }
        }

        public bool ResimSil(int resimId)
        {
            Resim resim = _resimRepository.GetSingle(r => r.Id == resimId);
            return _resimRepository.Remove(resim);
        }

        public Resim ResimGetir(int resimId)
        {
            return _resimRepository.GetSingle(r=>r.Id==resimId);
        }
    }
}
