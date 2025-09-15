using EmployeeLeaveManagementSystem.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EmployeeLeaveManagementSystem.Helpers
{
    public static class JwtHelper
    {
        //Generates a JWT token for a given user.
        //name=The name of the user (stored in token claims)
        //role=The role of the user (Admin / Employee)
        //IConfiguration=Application configuration (used to read JWT settings from appsettings.json)
        //A signed JWT token as a string

        public static string GenerateToken(string name,string role,IConfiguration config)
        {
            //Define claims (pieces of info about the user stored inside the token)
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, role)
            };

            //Create a secret key (read from appsettings.json)
            // This key is used to digitally sign the token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));

            //Define signing credentials(which algorithm and key to use)
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Create the JWT token object
           var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],      // Who issued the token (your app)
                audience: config["Jwt:Audience"],  // Who can use the token (your client)
                claims: claims,                    // User info (name + role)
                expires: DateTime.Now.AddHours(1), // Expiration time (1 hour from now)
                signingCredentials: creds);        //// Digital signature for security


            //Convert the JWT object into a string (the actual token sent to client)
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}


//Claims → Store user info inside token (e.g., Name, Role).

//Key → Secret string (from appsettings.json) used to sign the token.

//SigningCredentials → Defines algorithm (HmacSha256) + key.

//JwtSecurityToken → Creates the actual JWT object.

//WriteToken → Converts it into a string to return to client.


////So when user logs in:

//AuthService calls JwtHelper.GenerateToken(user.Name, user.Role, _config)

//It returns a JWT token string → which the client uses in Authorization: Bearer<token> header.