using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaret.Entities
{
   public class SepetUrun
    {
        public int UrunId { get; set; }
        public int SepetId { get; set; }
        public int Adet { get; set; }
        public Urun Urun { get; set; }
        public Sepet Sepet { get; set; }
    }
}
