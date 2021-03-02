using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ETicaret.Entities.Authentications.ViewModels.AppUserVM
{
    public class AppUserLoginVM
    {
        [Required(ErrorMessage ="Lütfen boş geçmeyiniz")]
        public string UsernameorEmail { get; set; }
        [Required(ErrorMessage ="Lütfen boş geçmeyiniz")]
        public string Password { get; set; }
    }
}
