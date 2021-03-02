using ETicaret.BL.Policies.Requirements;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.BL.Policies.Handles
{
    public class LoginTimeHandler : AuthorizationHandler<LoginTimeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, LoginTimeRequirement requirement)
        {
            Claim claim = context.User.Claims.FirstOrDefault(c => c.Type == "loginTime");
            if (claim != null)
            {
                DateTime loginTime = DateTime.Parse(claim.Value);
                TimeSpan tarihFarki = (DateTime.UtcNow - loginTime);

                if (tarihFarki.Minutes < 1)
                {
                    context.Succeed(requirement);
                }
                else
                    context.Fail();

            }
            return Task.CompletedTask;
        }
        
    }
}
