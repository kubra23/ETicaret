using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaret.Entities.Authentications.ViewModels.AppUserVM
{
    public class AppUserListeleVM
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<string> Roller { get; set; }
    }
}
