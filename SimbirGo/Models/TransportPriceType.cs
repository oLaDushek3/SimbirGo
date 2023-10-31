using System;
using System.Collections.Generic;

namespace TestApi.Models;

public partial class TransportPriceType
{
    public int TransportPriceTypeId { get; set; }

    public int TransportId { get; set; }

    public int PriceTypeId { get; set; }

    public double Price { get; set; }

    public virtual PriceType? PriceType { get; set; }

    public virtual ICollection<Rent> Rents { get; set; } = new List<Rent>();

    public virtual Transport? Transport { get; set; }
}
