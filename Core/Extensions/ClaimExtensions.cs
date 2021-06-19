using Core.Entities.Concrete;
using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Extensions
{
    public static class ClaimExtensions
    {
        public static void AddEmail(this ICollection<Claim> claims,String email)
        {
             claims.Add(new Claim(JwtRegisteredClaimNames.Email, email));
        }

        public static void AddName(this ICollection<Claim> claims, String name)
        {
            claims.Add(new Claim(ClaimTypes.Name, name));
           // claims.Add(new Claim(JwtRegisteredClaimNames.Name, name));
        }

        public static void AddNameIdentifier(this ICollection<Claim> claims, String nameIdentifier)
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdentifier));
            //claims.Add(new Claim(JwtRegisteredClaimNames.NameId, nameIdentifier));
        }

        public static void AddRoles(this ICollection<Claim> claims, String[] roles)
        {
            roles.ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
        }
    }
}
