using Microsoft.AspNetCore.Authorization;

namespace MasterDataManagement.API.Extensions
{
    public class RoleClaimsRequirements : IAuthorizationRequirement
    {
        public string ClaimValue { get; }
        public RoleClaimsRequirements(string _ClaimValue)
        {
            ClaimValue = _ClaimValue;
        }
    }
}