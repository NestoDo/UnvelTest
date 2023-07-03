using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umvel.Contracts.DTO.Sale
{
    public class RegisterSaleRequest
    {
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public decimal Total { get; set; }
    }
}
