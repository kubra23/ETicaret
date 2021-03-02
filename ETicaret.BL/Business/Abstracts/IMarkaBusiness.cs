using ETicaret.Entities.ViewModels.MarkaVM;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaret.BL.Business.Abstracts
{
    public interface IMarkaBusiness
    {
        List<MarkaListeleVM> Listele();
    }
}
