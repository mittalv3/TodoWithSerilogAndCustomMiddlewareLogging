using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Todo.API.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        ///
        public class AuthenticationRequestBody
        {
            /// <summary>
            /// Username to access the resource
            /// </summary>
            public string? Username { get; set; }
            /// <summary>
            /// Password to access the resource
            /// </summary>
            public string? Password { get; set; }
        }

        private class ToDosUsers
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public ToDosUsers(int id, string userName, string firstName, string lastName) 
            {
                Id = id;
                Username = userName;
                FirstName = firstName;
                LastName = lastName;
            }
        }

        private readonly IConfiguration _configuration;
        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate( AuthenticationRequestBody authenticationRequestBody)
        {
            var userDetails = ValidateUserCredentails(authenticationRequestBody.Username,
                authenticationRequestBody.Password);

            if (userDetails == null)
            {
                return Unauthorized();
            }

            var securityKey = new SymmetricSecurityKey(
                Convert.FromBase64String(_configuration["Authetication:SecretForKey"]));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", userDetails.Id.ToString()));
            claimsForToken.Add(new Claim("given_name", userDetails.FirstName));
            claimsForToken.Add(new Claim("family_name", userDetails.LastName));

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authetication:Issuer"],
                _configuration["Authetication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials);

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return tokenToReturn;
        }

        private ToDosUsers ValidateUserCredentails(string? username, string? password)
        {
            // As we don't have USER DB, So instead of verfying the credentails
            // Returing new ToDosUser assuming Credetials are Valid.
            return new ToDosUsers(1, username ?? "", "Vikas", "Mittal");
        }
    }


}
