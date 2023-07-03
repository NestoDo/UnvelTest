using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umvel.Contracts.DTO.Customer
{
    public class GetAllCustomerResponse
    {
        public int CustomerId { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
