using ETicaret.BL.Business.Abstracts;
using ETicaret.Entities.Authentications;
using ETicaret.Entities.Authentications.ViewModels.AppRoleVM;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.BL.Business
{
    public class RoleBusiness : IRoleBusiness
    {
        RoleManager<AppRole> _roleManager;
        public RoleBusiness(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;

        }


        public async Task<bool> EkleAsync(AppRoleEkleVM model)
        {
            AppRole appRole = new AppRole
            {
                Name = model.Name
            };
            IdentityResult result = await _roleManager.CreateAsync(appRole);
            return result.Succeeded; 
        }

        public List<AppRoleListeVM> RolleriGetir()
        {
           return _roleManager.Roles.Select(r => new AppRoleListeVM
            {
                Id = r.Id,
                Name = r.Name
            }).ToList();
        }

        public async Task<bool> SilAsync(int id)
        {
            AppRole appRole = await _roleManager.FindByIdAsync(id.ToString());
            IdentityResult result = await _roleManager.DeleteAsync(appRole);
            return result.Succeeded;
        }

        public async Task<bool> SilAsync(string name)
        {
            AppRole appRole = await _roleManager.FindByNameAsync(name);
            IdentityResult result = await _roleManager.DeleteAsync(appRole);
            return result.Succeeded;
        }
    }
}
