using ETicaret.BL.Business.Abstracts;
using ETicaret.BL.Repositories.Abstracts;
using ETicaret.BL.Repositories.Concretes;
using ETicaret.Entities;
using ETicaret.Entities.ViewModels.YorumVM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETicaret.BL.Business
{
    public class YorumBusiness : IYorumBusiness
    {
        IYorumRepository _yorumRepository;
        public YorumBusiness()
        {
            _yorumRepository = new YorumRepository();
        }
        public bool OnayDurumDegistir(bool durum, int yorumId)
        {
            Yorum yorum = _yorumRepository.GetSingle(y => y.Id == yorumId);
            yorum.Onay = durum;
            return _yorumRepository.Update(yorum);
        }

        public List<YorumListeleVM> TumYorumlar()
        {
            return _yorumRepository.GetTable().Include(y => y.Urun).Select(y => new YorumListeleVM
            {
                Adi = y.Adi,
                Email = y.Email,
                Mesaj = y.Mesaj,
                Onay = y.Onay,
                Soyadi = y.Soyadi,
                Id = y.Id,
                Urun = y.Urun
            }).ToList();
        }
    }
}
