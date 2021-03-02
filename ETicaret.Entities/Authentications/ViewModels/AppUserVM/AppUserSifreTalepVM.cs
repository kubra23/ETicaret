using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ETicaret.Entities.Authentications.ViewModels.AppUserVM
{
    public class AppUserSifreTalepVM
    {
        [Required(ErrorMessage ="Lütfen boş geçmeyiniz.")]
        public string Email { get; set; }
    }
}
