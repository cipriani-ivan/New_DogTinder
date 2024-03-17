using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NewDogTinder.Controller.Controllers;

[Route("[controller]")]
[ApiController]
public partial class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _configuration;

    private class CityInfoUser
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }

        public CityInfoUser(
            string userName, 
            string firstName, 
            string lastName, 
            string city)
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            City = city;
        }

    }

    public AuthenticationController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("authenticate")]
    public ActionResult<string> Authenticate(
        AuthenticationRequestBody authenticationRequestBody)
    {  
        // Step 1: validate the username/password
        var user = ValidateUserCredentials(
            authenticationRequestBody.UserName,
            authenticationRequestBody.Password);

        if (user == null)
        {
            return Unauthorized();
        }

        // Step 2: create a token
        var securityKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
        var signingCredentials = new SigningCredentials(
            securityKey, SecurityAlgorithms.HmacSha256);

        var claimsForToken = new List<Claim>
        {
            new Claim("given_name", user.FirstName),
            new Claim("family_name", user.LastName),
            new Claim("city", user.City)
        };

        var jwtSecurityToken = new JwtSecurityToken(
            _configuration["Authentication:Issuer"],
            _configuration["Authentication:Audience"],
            claimsForToken,
            DateTime.UtcNow,
            DateTime.UtcNow.AddHours(1),
            signingCredentials);

        var tokenToReturn = new JwtSecurityTokenHandler()
           .WriteToken(jwtSecurityToken);

        return Ok(tokenToReturn);
    }

    private CityInfoUser ValidateUserCredentials(string? userName, string? password)
    {
        // TODO: to save in a database
        return new CityInfoUser(
            userName ?? "",
            "string",
            "string",
            "Trondheim");
    }
}
