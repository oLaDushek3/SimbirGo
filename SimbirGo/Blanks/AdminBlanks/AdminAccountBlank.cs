using System.ComponentModel.DataAnnotations;

namespace TestApi.Blanks.AdminBlanks;

public class AdminAccountBlank
{
    [Required]
    public string Username { get; set; } = null!;
    
    [Required]
    public string Password { get; set; } = null!;
    
    [Required]
    public bool IsAdmin { get; set; }
    
    [Required]
    public double Balance { get; set; }
}