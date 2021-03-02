using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaret.Entities.Authentications.ViewModels
{
   public class TwoFactorLoginVM
    {
        public string DogrulamaKodu { get; set; }
        public bool KurtarmaKodumu { get; set; }
    }
}
