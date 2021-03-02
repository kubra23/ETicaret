using ETicaret.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaret.Entities.Authentications
{
    public class AppUser : IdentityUser<int>
    {
        public string TuttuguTakim { get; set; }
        public TwoFactorType TwoFactorType { get; set; }
        public Sepet Sepet { get; set; }
    }
}
