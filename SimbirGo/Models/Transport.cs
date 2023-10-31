using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TestApi.Models;

public partial class Transport
{
    public int TransportId { get; set; }

    public int? OwnerId { get; set; }

    public bool CanBeRented { get; set; }

    public int? TransportModelId { get; set; }

    public int? ColorId { get; set; }

    public string Identifier { get; set; } = null!;

    public string? Description { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public virtual Color? Color { get; set; }

    [JsonIgnore]
    public virtual Account? Owner { get; set; }

    public virtual TransportModel? TransportModel { get; set; }

    public virtual List<TransportPriceType> TransportPriceTypes { get; set; } = new List<TransportPriceType>();
}
