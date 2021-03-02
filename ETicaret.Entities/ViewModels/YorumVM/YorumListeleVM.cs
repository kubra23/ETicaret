using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaret.Entities.ViewModels.YorumVM
{
   public class YorumListeleVM
    {
        public int Id { get; set; }
        public Urun Urun { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string Email { get; set; }
        public string Mesaj { get; set; }
        public bool Onay { get; set; }
    }
}
