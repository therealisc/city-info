using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace CityInfo.API.Controllers;

[Route("api/authentication")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    public class AuthenticationRequestBody
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }

    private class CityInfoUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }

        public CityInfoUser(int userId, string userName, string firstName, string lastName, string city)
        {
            UserId = userId;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            City = city;
        }
    }

    private readonly IConfiguration _configuration;

    public AuthenticationController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("authenticate")]
    public ActionResult<string> Authenticate(
            AuthenticationRequestBody authenticationRequestBody)
    {
        var user = ValidateUserCredentials(
                authenticationRequestBody.UserName, authenticationRequestBody.Password);

        if (user == null)
        {
            return Unauthorized();
        }

        var securityKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));

        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claimsForToken = new List<Claim>();
        claimsForToken.Add(new Claim("sub", user.UserId.ToString(), null));
        claimsForToken.Add(new Claim("given_name", user.FirstName, null ));
        claimsForToken.Add(new Claim("family_name", user.LastName, null));
        claimsForToken.Add(new Claim("city", user.City, null));

        var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials);

        var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return Ok(tokenToReturn);
    }

    private CityInfoUser ValidateUserCredentials(string? userName, string? password)
    {
        return new CityInfoUser(1, userName ?? "", "Ioan", "Scafa", "Antwerp");
    }
}
