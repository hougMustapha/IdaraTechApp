using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace IdaraTechApi
{
    public static class SD
    {
        public const string Facebook = "facebook";
        public const string Google = "google";

        //Roles
        public const string SuperAdminRole = "SuperAdmin";
        public const string AdminRole = "Admin";
        public const string ManagerRole = "Manager";
        public const string CustomerRole = "Customer";
        public const string CitizenRole = "Citoyen";

        public const string SuperAdminUserName = "superAdmin@example.com";
        public const string SuperAdminChangeNotAllowed = "Super Admin change is not allowed";
        public const int MaximumLoginAttempts = 3;

        public static bool SHIPPolicy(AuthorizationHandlerContext context)
        {
            if (context.User.IsInRole(CitizenRole) &&
                context.User.HasClaim(c => c.Type == ClaimTypes.Email && c.Value.Contains("ship")))
            {
                return true;
            }

            return false;
        }

    }
}
