using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TestApi.Models;

public partial class PriceType
{
    public int PriceTypeId { get; set; }

    public string Name { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<TransportPriceType> TransportPriceTypes { get; set; } = new List<TransportPriceType>();
}
