using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Product { get; }
        IUserRepository User { get; }
        IWishListRepository WishList { get; }
        ICategoryRepository Category { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
