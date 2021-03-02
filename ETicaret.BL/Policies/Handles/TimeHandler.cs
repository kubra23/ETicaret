using ETicaret.BL.Policies.Requirements;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.BL.Policies.Handles
{
    public class TimeHandler : AuthorizationHandler<TimeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TimeRequirement requirement)
        {
            if (DateTime.UtcNow.Minute>=40 && DateTime.UtcNow.Minute<=45)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
