using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ETicaret.Entities.Authentications.ViewModels.AppUserVM
{
    public class AppUserSifreGuncelleVM
    {
        [Required(ErrorMessage ="Lütfen boş geçmeyiniz.")]
        public string Sifre { get; set; }

        [Required(ErrorMessage ="Lütfen boş geçmeyiniz.")]
        [Compare("Sifre",ErrorMessage ="Girilen şifre ile eşleşmiyor.")]
        public string YeniSifre { get; set; }
    }
}
