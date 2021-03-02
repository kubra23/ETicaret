using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaret.Entities.Authentications.ViewModels
{
    public class VerifyState
    {
        public bool State { get; set; }
        public List<string> RecoveryCodes { get; set; }
    }
}
