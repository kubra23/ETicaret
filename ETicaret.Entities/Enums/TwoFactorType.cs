using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ETicaret.Entities.Enums
{
    public enum TwoFactorType
    {
        [Display(Name = "Microsoft & Google Authenticator İle Doğrulama")]
        Authenticator,
        [Display(Name = "SMS İle Doğrulama")]
        SMS,
        [Display(Name = "EMail İle Doğrulama")]
        Email
    }
}
