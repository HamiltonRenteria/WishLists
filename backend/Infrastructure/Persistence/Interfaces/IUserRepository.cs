using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AccountByUserName(string userName);
        Task<int> CreateAsync(User account);
    }
}
