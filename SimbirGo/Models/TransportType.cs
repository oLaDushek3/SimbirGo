using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TestApi.Models;

public partial class TransportType
{
    public int TransportTypeId { get; set; }

    public string Name { get; set; } = null!;
    
    [JsonIgnore]
    public virtual ICollection<TransportModel> TransportModels { get; set; } = new List<TransportModel>();
}
