using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ETicaret.Entities
{
    public class Yorum
    {
        public int Id { get; set; }
        public int UrunId { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string Email { get; set; }
        public string Mesaj { get; set; }
        public bool Onay { get; set; }
        public Urun Urun { get; set; }

    }
}
