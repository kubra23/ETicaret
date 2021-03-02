using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ETicaret.Entities.Authentications.ViewModels.AppUserVM
{
   public class AppUserKayitOlVM
    {
        [Required(ErrorMessage ="Lütfen boş geçmeyiniz")]
        [StringLength(20,MinimumLength =3,ErrorMessage ="Lütfen 3 ile 20 karakter arasında giriniz.")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Lütfen boş geçmeyin.")]
        [EmailAddress(ErrorMessage ="Lütfen email adresi formatında doldurunuz.")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Lütfen boş geçmeyiniz.")]
        [DataType(DataType.Password,ErrorMessage ="Lütfen şifreyi kompleks giriniz.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Lütfen boş geçmeyiniz.")]
        [Compare("Password",ErrorMessage ="Lütfen şifreyi doğru giriniz.")]
        public string PasswordTekrar { get; set; }
    }
}
