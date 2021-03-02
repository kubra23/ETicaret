using ETicaret.Entities.Authentications.ViewModels.AppRoleVM;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.BL.Business.Abstracts
{
    public interface IRoleBusiness
    {
        List<AppRoleListeVM> RolleriGetir();
        Task<bool> EkleAsync(AppRoleEkleVM model);
        Task<bool> SilAsync(int id);
        Task<bool> SilAsync(string name);
        
    }
}
