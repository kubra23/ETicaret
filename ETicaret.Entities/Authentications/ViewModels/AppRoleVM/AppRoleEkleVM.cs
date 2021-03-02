using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ETicaret.Entities.Authentications.ViewModels.AppRoleVM
{
   public class AppRoleEkleVM
    {
        [Required(ErrorMessage ="Lütfen boş geçmeyiniz.")]
        public string Name { get; set; }
    }
}
