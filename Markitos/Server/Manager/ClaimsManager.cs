using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Markitos.Server.Manager
{
    public class ClaimsManager
    {
        //public static UserInfoFromClaimsModel GetClaimUserInfo(ClaimsPrincipal user)
        //{
        //    UserInfoFromClaimsModel userClaims = new UserInfoFromClaimsModel()
        //    {
        //        TenantId = GetTenantID(user),
        //        UserGuid = GetUserObjectID(user),
        //    };
        //    GetUserUPNAndType(user, userClaims);
        //    return userClaims;
        //}

        public static string GetUserFirstName(ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.GivenName);
        }
        public static string GetUserLastName(ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.Surname);
        }

        public static string GetUserObjectID(ClaimsPrincipal user)
        {
            return user.FindFirst(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
        }

        private static void GetUserUPNAndType(ClaimsPrincipal user)
        {
            //userClaims.UserName = GetUserUPN(user);
            //userClaims.Type = AppConstants.USER;
            //if (string.IsNullOrWhiteSpace(userClaims.UserName))
            //{
            //    userClaims.UserName = GetAppID(user);
            //    userClaims.Type = AppConstants.SERVICEPRINCIPAL;
            //}
        }

        public static string GetUserUPN(ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.Name);
        }

        public static string GetAppID(ClaimsPrincipal user)
        {
            return user.FindFirst(c => c.Type == "appid").Value;
        }

        public static string GetUserEmail(ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.Email);
        }
    }
}
