using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaret.Entities
{
   public class SiparisDetay
    {
        public int SiparisId { get; set; }
        public int UrunId { get; set; }
        public int Adet { get; set; }
        public int İndirim { get; set; }
        public Siparis Siparis { get; set; }
        public Urun Urun { get; set; }
    }
}
