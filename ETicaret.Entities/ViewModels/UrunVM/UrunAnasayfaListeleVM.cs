using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaret.Entities.ViewModels.UrunVM
{
    public class UrunAnasayfaListeleVM
    {
        public int Id { get; set; }
        public string MarkaAdi { get; set; }
        public string KategoriAdi { get; set; }
        public string Adi { get; set; }
        public decimal Fiyat { get; set; }
        public string VitrinResmi { get; set; }
    }
}
