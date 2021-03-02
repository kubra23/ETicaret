using ETicaret.Entities.ViewModels.ResimVM;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaret.Entities.ViewModels.UrunVM
{
   public class UrunListeleVM
    {
        public int Id { get; set; }
        public string MarkaAdi { get; set; }
        public string KategoriAdi { get; set; }
        public string Adi { get; set; }
        public int StokAdet { get; set; }
        public decimal Fiyat { get; set; }
        public DateTime UretimTarihi{ get; set; }
        
    }
}
