namespace TestApi;

public record class JwtOptions(
    // lifetime validate params
    int RefreshTokenValidityInDays,
    int TokenValidityInMinutes,
    
    // params
    string ValidAudience,
    string ValidIssuer,
    string Secret
);