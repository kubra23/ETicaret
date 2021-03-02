using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ETicaret.Entities.ViewModels.YorumVM
{
   public class YorumEkleVM
    {
        public int UrunId { get; set; }
        [Required(ErrorMessage ="Lütfen adı boş geçmeyiniz")]
        public string Adi { get; set; }
        [Required(ErrorMessage ="Lütfen soyadı boş geçmeyiniz")]
        public string Soyadi { get; set; }
        [Required(ErrorMessage ="Lütfen mail adresini boş geçmeyiniz")]
        [EmailAddress(ErrorMessage ="Lütfen doğru bir mail adresi giriniz.")]
        public string Mail { get; set; }
        [Required(ErrorMessage ="Lütfen mesaj alanını boş geçmeyiniz")]
        public string Mesaj { get; set; }
    }
}
