using ETicaret.Entities.ViewModels.ResimVM;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaret.Entities.ViewModels.UrunVM
{
   public class UrunResimListeleVM
    {
        public string Adi { get; set; }
        public  List<ResimListeVM> Resimler { get; set; }
    }
}
