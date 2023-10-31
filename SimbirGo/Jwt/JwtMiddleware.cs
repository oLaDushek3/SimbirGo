using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TestApi.Repositories;

namespace TestApi.Jwt;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IAccountRepository accountRepository, IUsersSessionsRepository usersSessionsRepository, JwtOptions jwtOptions)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
            AttachAccountToContext(context, token, accountRepository, usersSessionsRepository, jwtOptions);

        await _next(context);
    }

    private static void AttachAccountToContext(HttpContext context, string token, IAccountRepository accountRepository, IUsersSessionsRepository usersSessionsRepository, JwtOptions jwtOptions)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtOptions.Secret);
        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = jwtOptions.ValidIssuer,
            ValidAudience = jwtOptions.ValidAudience,
            ClockSkew = TimeSpan.Zero
        }, out SecurityToken validatedToken);

        var jwtToken = (JwtSecurityToken)validatedToken;
        var accountId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

        // attach account to context on successful jwt validation
        if(!usersSessionsRepository.GetUsersSessionsByAccountId(accountId).ValidSession)
            return;
        context.Items["Account"] = accountRepository.GetAccountById(accountId);
    }
}