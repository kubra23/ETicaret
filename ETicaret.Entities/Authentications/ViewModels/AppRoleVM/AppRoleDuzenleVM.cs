using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ETicaret.Entities.Authentications.ViewModels.AppRoleVM
{
    public class AppRoleDuzenleVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Lütfen rol adını boş geçmeyiniz.")]
        [StringLength(20,MinimumLength =3,ErrorMessage ="Lütfen rol adını 3 ile 20 karakter arsında giriniz.")]
        public string RoleAdi { get; set; }
    }
}
