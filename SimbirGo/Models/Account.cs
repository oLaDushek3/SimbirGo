using System.Text.Json.Serialization;

namespace TestApi.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string Username { get; set; } = null!;

    [JsonIgnore]
    public string Password { get; set; } = null!;

    public bool IsAdmin { get; set; }

    public double Balance { get; set; }

    public virtual ICollection<Rent> Rents { get; set; } = new List<Rent>();
    
    [JsonIgnore]
    public virtual ICollection<UsersSessions> UsersSessions { get; set; } = new List<UsersSessions>();
    
    [JsonIgnore]
    public virtual ICollection<Transport> Transports { get; set; } = new List<Transport>();
}
