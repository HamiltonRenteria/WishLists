using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Static;

namespace Infrastructure.Persistence.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DvpDbContext _context;

        public UserRepository(DvpDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> AccountByUserName(string userName)
        {
            var userAccount = await _context.Users!
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email.Equals(userName) && x.State.Equals(Convert.ToBoolean((int)StateTypes.Active)));

            return userAccount!;
        }
    }
}
