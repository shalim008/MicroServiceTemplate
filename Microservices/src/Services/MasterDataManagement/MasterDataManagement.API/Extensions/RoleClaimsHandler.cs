using Microsoft.AspNetCore.Authorization;

namespace MasterDataManagement.API.Extensions
{
    public class RoleClaimsHandler : AuthorizationHandler<RoleClaimsRequirements>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleClaimsRequirements requirement)
        {
            var claim = context.User.FindFirst(requirement.ClaimValue);
            if (claim != null)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}