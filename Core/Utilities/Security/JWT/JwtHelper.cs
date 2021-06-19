using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Core.Utilities.Security.Encryption;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Core.Extensions;
using System.Linq;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get;}
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;

        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        }

        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);

            var jwt = CreateJwtSecurityToken(user,_tokenOptions, operationClaims, signingCredentials);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken()
            {
                Expiration = _accessTokenExpiration,
                Token = token
            };
        }

       public JwtSecurityToken CreateJwtSecurityToken(User user,TokenOptions tokenOptions,
           List<OperationClaim> operationClaims,SigningCredentials signingCredentials)
        {
            var jwt = new JwtSecurityToken(
                issuer:_tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                claims: SetClaims(user,operationClaims),
                notBefore:DateTime.Now,
                expires:_accessTokenExpiration,
                signingCredentials: signingCredentials
                );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user,List<OperationClaim> operationClaims )
        {
            var claims = new List<Claim>();
            // claims.Add(new Claim("email", user.Email));
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddRoles(operationClaims.Select(c=>c.Name).ToArray());

            return claims;
        }

    }
}
