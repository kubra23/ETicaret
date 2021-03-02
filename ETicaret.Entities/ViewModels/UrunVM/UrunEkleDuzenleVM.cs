using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ETicaret.Entities.ViewModels.UrunVM
{
    public class UrunEkleDuzenleVM
    {
        public int MarkaId { get; set; }
        public int KategoriId { get; set; }
        [Required(ErrorMessage ="Lütfen ürün adını boş geçmeyiniz.")]
        [StringLength(100,MinimumLength =2,ErrorMessage ="Lütfen ürün adını 2 ile 100 karakter arasında giriniz.")]
        public string Adi { get; set; }
        [Range(0,1000000,ErrorMessage ="Lütfen 0 ile 1000000 arasında stok adedi belirtiniz. ")]
        [Required(ErrorMessage ="Lütfen stok adedini boş geçmeyiniz.   ")]
        public int? StokAdet { get; set; }
        [Required(ErrorMessage ="Lütfen ürünün fiyatını boş geçmeyiniz")]
        public decimal? Fiyat { get; set; }
        [Required(ErrorMessage ="Lütfen üretim tarihini boş geçmeyiniz.")]
        public DateTime? UretimTarihi { get; set; }

    }
}
