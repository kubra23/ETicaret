using ETicaret.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ETicaret.Entities.Authentications.ViewModels.AppUserVM
{
   public  class AppUserGuncelleVM
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string TuttuguTakim { get; set; }
        public string CurrentPassword { get; set; }
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Lütfen şifreyi doğrulayınız.")]
        public string PasswordTekrar { get; set; }
        public TwoFactorType TwoFactorType { get; set; }
        public bool TwoFactorEnabled { get; set; }
    }
}
