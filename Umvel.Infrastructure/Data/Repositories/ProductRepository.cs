using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umvel.Infrastructure.Data.Repositories.Interfaces;
using Umvel.Infrastructure.Database.Models;

namespace Umvel.Infrastructure.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly UmveltestContext _context;

        public ProductRepository(UmveltestContext context)
        {
            _context = context;
        }        

        public IEnumerable<Product> GetAll()
        {
            return _context.Products;
        }

        public Product GetById(int id)
        {
            return _context.Products.Find(id);
        }

        public Product Add(Product customer)
        {
            _context.Add(customer);
            _context.SaveChanges();

            return customer;
        }
    }
}
