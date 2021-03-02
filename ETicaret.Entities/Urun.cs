using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaret.Entities
{
    public class Urun
    {
        public int Id { get; set; }
        public int MarkaId { get; set; }
        public int KategoriId { get; set; }
        public int VitrinResmiId { get; set; }
        public string UrunAdi { get; set; }
        public int StokAdet { get; set; }
        public decimal Fiyat { get; set; }
        public DateTime UretimTarihi { get; set; }


        public Kategori Kategori { get; set; }
        public Marka Marka { get; set; }
        public  ICollection<Yorum>Yorumlar { get; set; }
        public  ICollection<SiparisDetay>Siparisler { get; set; }
        public  ICollection<SepetUrun>Sepetler { get; set; }
        public  ICollection<Resim>Resimler { get; set; }


    }
}
