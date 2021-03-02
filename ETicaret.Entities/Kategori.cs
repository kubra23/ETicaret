using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaret.Entities
{
   public class Kategori
    {
        public int Id { get; set; }
        public string KategoriAdi { get; set; }

        public ICollection<Urun> Urunler { get; set; }
    }
}
