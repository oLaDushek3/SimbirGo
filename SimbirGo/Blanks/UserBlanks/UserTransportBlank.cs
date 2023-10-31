using System.ComponentModel.DataAnnotations;

namespace TestApi.Blanks.UserBlanks;

public class UserTransportBlank
{
    [Required]
    public bool CanBeRented { get; set; }
    
    [Required]
    public string TransportType { get; set; } = null!;
    
    [Required]
    public string Model { get; set; } = null!;
    
    [Required]
    public string Color { get; set; } = null!;
    
    [Required]
    public string Identifier { get; set; } = null!;

    public string? Description { get; set; }
    
    [Required]
    public double Latitude { get; set; }
    
    [Required]
    public double Longitude { get; set; }
    
    [Required]
    public double MinutePrice { get; set; }
    
    [Required]
    public double DayPrice { get; set; }
}