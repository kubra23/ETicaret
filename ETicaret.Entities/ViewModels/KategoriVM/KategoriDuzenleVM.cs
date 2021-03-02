using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ETicaret.Entities.ViewModels.KategoriVM
{
    public class KategoriDuzenleVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Lütfen kategori adını boş geçmeyiniz")]
        [StringLength(20,MinimumLength =3,ErrorMessage ="Lütfen kategori adını 3 ile 20 karakter arasında giriniz")]
        public string KategoriAdi { get; set; }
    }
}
