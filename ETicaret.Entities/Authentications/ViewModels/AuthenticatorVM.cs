using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaret.Entities.Authentications.ViewModels
{
    public class AuthenticatorVM 
    {
        public string SharedKey { get; set; }
        public string QrCodeUri { get; set; }
        public string VerificationCode { get; set; }
    }

}
