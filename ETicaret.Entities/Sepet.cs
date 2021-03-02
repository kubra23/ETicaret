using ETicaret.Entities.Authentications;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaret.Entities
{
    public class Sepet
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public  ICollection<SepetUrun>Urunler{ get; set; }
        public AppUser AppUser { get; set; }
    }
}
