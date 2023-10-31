using System.ComponentModel.DataAnnotations;

namespace TestApi.Blanks.UserBlanks;

public class UserAccountBlank
{
    [Required]
    public string Username { get; set; } = null!;
    
    [Required]
    public string Password { get; set; } = null!;
}