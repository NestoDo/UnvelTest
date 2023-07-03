using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umvel.Contracts.DTO;
using Umvel.Infrastructure.Database.Models;
using Umvel.Infrastructure.Repositories.Interfaces;

namespace Umvel.Infrastructure.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly UmveltestContext _context;

        public CustomerRepository(UmveltestContext context)
        {
            _context = context;
        }

        public IEnumerable<Customer> GetAll()
        {

            return _context.Customers;
        }

        public Customer GetById(int id)
        {
            return _context.Customers.Find(id);
        }

        public Customer Add(Customer customer)
        {
            _context.Add(customer);
            _context.SaveChanges();

            return customer;
        }        
    }
}
