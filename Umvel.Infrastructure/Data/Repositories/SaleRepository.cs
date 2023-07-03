using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umvel.Infrastructure.Data.Repositories.Interfaces;
using Umvel.Infrastructure.Database.Models;

namespace Umvel.Infrastructure.Data.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly UmveltestContext _context;

        public SaleRepository(UmveltestContext context)
        {
            _context = context;
        }

        public Sale Add(Sale sale)
        {
            _context.Add(sale);
            _context.SaveChanges();

            return sale;
        }

        public IEnumerable<Sale> GetSaleByDateRange(DateTime StartDate, DateTime EndDate)
        {
            return _context.Sales.Where(s => s.Date >= StartDate && s.Date <= EndDate);
        }
    }
}
