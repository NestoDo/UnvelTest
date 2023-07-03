using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umvel.Contracts.DTO.Product
{
    public class GetProductByIdResponse
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string UnitPrice { get; set; }
        public decimal Cost { get; set; }
    }
}
