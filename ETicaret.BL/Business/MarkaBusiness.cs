using ETicaret.BL.Business.Abstracts;
using ETicaret.BL.Repositories.Abstracts;
using ETicaret.BL.Repositories.Concretes;
using ETicaret.Entities.ViewModels.MarkaVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETicaret.BL.Business
{
    public class MarkaBusiness : IMarkaBusiness
    {
        IMarkaRepository _markaRepository;

        public MarkaBusiness()
        {
            _markaRepository = new MarkaRepository();
        }

        public List<MarkaListeleVM> Listele()
        {
            return _markaRepository.Get().Select(m => new MarkaListeleVM
            {
                Id = m.Id,
                MarkaAdi = m.MarkaAdi
            }).ToList();
        }
    }
}
