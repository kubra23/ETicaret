using ETicaret.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaret.Entities.Authentications.ViewModels
{
    public class TwoFactorTypeSelectVM
    {
        public TwoFactorType TwoFactorType { get; set; }
        public bool TwoFactorEnabled { get; set; }
    }
}
