using System;
using System.Collections.Generic;

namespace Umvel.Infrastructure.Database.Models;

public partial class Concept
{
    public int ConceptId { get; set; }

    public decimal Quantity { get; set; }

    public int ProductId { get; set; }

    public int SaleId { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal Amount { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Sale Sale { get; set; } = null!;
}
