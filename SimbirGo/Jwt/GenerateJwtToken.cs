using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TestApi.Models;
using TestApi.Repositories;

namespace TestApi.Jwt;

public class GenerateJwtToken
{
    private readonly JwtOptions _jwtOptions;
    private IUsersSessionsRepository _usersSessionsRepository;

    public GenerateJwtToken(JwtOptions jwtOptions, IUsersSessionsRepository usersSessionsRepository)
    {
        _jwtOptions = jwtOptions;
        _usersSessionsRepository = usersSessionsRepository;
    }

    public string GetJwtToken(Account account)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", account.AccountId.ToString()), new Claim("isAdmin", account.IsAdmin.ToString())}),
            Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.TokenValidityInMinutes),
            Issuer = _jwtOptions.ValidIssuer,
            Audience = _jwtOptions.ValidAudience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        _usersSessionsRepository.InsertUsersSessions(new UsersSessions { AccountId = account.AccountId, ValidSession = true});
        
        return tokenHandler.WriteToken(token);
    }
}