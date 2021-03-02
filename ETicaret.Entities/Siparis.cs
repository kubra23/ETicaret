using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaret.Entities
{
   public class Siparis
    {
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public DateTime SiparişTarihi { get; set; }
        public  ICollection<SiparisDetay>Detaylar { get; set; }
    }
}
