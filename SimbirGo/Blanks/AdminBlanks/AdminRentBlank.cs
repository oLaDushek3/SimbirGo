using System.ComponentModel.DataAnnotations;

namespace TestApi.Blanks.AdminBlanks;

public class AdminRentBlank
{
    [Required]
    public long TransportId { get; set; }
    
    [Required]
    public long UserId { get; set; }
    
    [Required]
    public string TimeStart { get; set; }
    
    public string? TimeEnd { get; set; }
    
    [Required]
    public double PriceOfUnit { get; set; }
    
    [Required]
    public string PriceType { get; set; }

    public double? FinalPrice { get; set; }
}