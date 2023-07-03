using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umvel.Infrastructure.Database.Models;

namespace Umvel.Infrastructure.Data.Repositories.Interfaces
{
    public interface ISaleRepository
    {
        Sale Add(Sale sale);
        IEnumerable<Sale> GetSaleByDateRange(DateTime StartDate, DateTime EndDate);
    }
}
