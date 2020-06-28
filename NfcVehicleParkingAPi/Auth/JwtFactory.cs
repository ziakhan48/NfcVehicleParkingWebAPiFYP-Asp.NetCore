using NfcVehicleParkingAPi.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace NfcVehicleParkingAPi.Auth
{
    public class JwtFactory : IJwtFactory
    {
       
        private readonly JwtIssuerOptions _jwtOptions;
        private UserManager<AppUser> _userManager;

        public JwtFactory(Microsoft.Extensions.Options.IOptions<JwtIssuerOptions> jwtOptions,
            UserManager<AppUser> userManager
            )
        {
            _jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_jwtOptions);
            _userManager = userManager;
        }

        public async Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity)
        {
            IdentityOptions _option = new IdentityOptions();
            
            var userbyname = _userManager.FindByNameAsync(userName).Result;
            var userrole = _userManager.GetRolesAsync(userbyname).Result;
            var claims = new List<Claim>
         {
                 new Claim(JwtRegisteredClaimNames.Sub, userName ,JwtRegisteredClaimNames.Acr ,userrole.FirstOrDefault()),
                 new Claim(JwtRegisteredClaimNames.Typ ,userrole.FirstOrDefault()),
                 new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                 new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                 //new Claim(_option.ClaimsIdentity.RoleClaimType, userrole.FirstOrDefault()),
                 identity.FindFirst(Helpers.Constants.Strings.JwtClaimIdentifiers.Rol),
                 identity.FindFirst(Helpers.Constants.Strings.JwtClaimIdentifiers.Id),
                  new Claim(_option.ClaimsIdentity.RoleClaimType, userrole.FirstOrDefault()),
                   
             };

            AddRolesToClaims(claims, userrole);

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public ClaimsIdentity GenerateClaimsIdentity(string userName, string id)
        {

            //IdentityOptions _option = new IdentityOptions();
            //var userbyname =  _userManager.FindByNameAsync(userName).Result;
            //var userrole =  _userManager.GetRolesAsync(userbyname).Result;
            var claim= new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
        {
                new Claim(Helpers.Constants.Strings.JwtClaimIdentifiers.Id, id),
                new Claim(Helpers.Constants.Strings.JwtClaimIdentifiers.Rol, Helpers.Constants.Strings.JwtClaims.ApiAccess),
               //claims.AddClaim(new Claim(Helpers.Constants.Strings.JwtClaimIdentifiers.Roles, role));
            //new Claim(_option.ClaimsIdentity.RoleClaimType, userrole.FirstOrDefault()),
            });

                       //claim.AddClaim(new Claim(ClaimTypes.Role, userrole.FirstOrDefault()));

           

            return claim;

        }

        private void AddRolesToClaims(List<Claim> claims, IEnumerable<string> roles)
        {
            foreach (var role in roles)
            {
                var roleClaim = new Claim(ClaimTypes.Role, role);
                claims.Add(roleClaim);
            }
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() -
                               new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                              .TotalSeconds);

        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
            }
        }

    }
}
