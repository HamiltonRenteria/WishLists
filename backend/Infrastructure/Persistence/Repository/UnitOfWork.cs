using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DvpDbContext _context;
        public IProductRepository Product { get; private set; }

        public IUserRepository User { get; private set; }

        public IWishListRepository WishList { get; private set; }

        public ICategoryRepository Category { get; private set; }

        public UnitOfWork(DvpDbContext context, IConfiguration configuration)
        {
            _context = context;
            Product = new ProductRepository(_context);
            User = new UserRepository(_context);
            WishList = new WishListRepository(_context);
            Category = new CategoryRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
