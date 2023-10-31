using System;
using System.Collections.Generic;

namespace TestApi.Models;

public partial class Rent
{
    public int RentId { get; set; }

    public DateTime TimeStart { get; set; }

    public DateTime? TimeEnd { get; set; }

    public double PriceOfUnit { get; set; }

    public int? TransportPriceTypeId { get; set; }

    public double? FinalPrice { get; set; }

    public int AccountId { get; set; }

    public virtual Account? Account { get; set; }

    public virtual TransportPriceType? TransportPriceType { get; set; }
}
