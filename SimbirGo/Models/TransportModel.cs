using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TestApi.Models;

public partial class TransportModel
{
    public int TransportModelId { get; set; }

    public int? TransportTypeId { get; set; }

    public string Name { get; set; } = null!;
 
    [JsonIgnore]
    public virtual ICollection<Transport> Transports { get; set; } = new List<Transport>();

    public virtual TransportType? Type { get; set; }
}
