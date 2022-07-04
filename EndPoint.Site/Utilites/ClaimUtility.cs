using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EndPoint.Site.Utilites
{
    public static class ClaimUtility
    {
        public static long? GetUserId(ClaimsPrincipal user)
        {
            try
            {
                if (user.Identity.IsAuthenticated == true)
                {
                    var claimIdentity = user.Identity as ClaimsIdentity;
                    long userId = long.Parse(claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
                    return userId;
                }
                return 0;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public static string GetUserEmail(ClaimsPrincipal user)
        {
            try
            {
                var claimIdentity = user.Identity as ClaimsIdentity;
               return claimIdentity.FindFirst(ClaimTypes.Email).Value;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public static List<string> GetRoles(ClaimsPrincipal user)
        {
            try
            {
                var claimIdentity = user.Identity as ClaimsIdentity;
                List<string> roles = new List<string>();
                foreach (var item in claimIdentity.Claims.Where(p => p.Type.EndsWith("role")))
                {
                    roles.Add(item.Value);
                }
                return roles;
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
