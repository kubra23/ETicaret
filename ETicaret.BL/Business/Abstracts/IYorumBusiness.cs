using ETicaret.Entities.ViewModels.YorumVM;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaret.BL.Business.Abstracts
{
    public interface IYorumBusiness
    {
        public List<YorumListeleVM> TumYorumlar();
        public bool OnayDurumDegistir(bool durum, int yorumId);
    }
}
