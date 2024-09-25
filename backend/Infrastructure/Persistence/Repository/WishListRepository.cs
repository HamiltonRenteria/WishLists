using Domain.Entities;
using Infrastructure.Commons.Bases.Request;
using Infrastructure.Commons.Bases.Response;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repository
{
    public class WishListRepository : GenericRepository<Wishlist>, IWishListRepository
    {
        public WishListRepository(DvpDbContext context) : base(context) { }

        public async Task<BaseEntityResponse<Wishlist>> ListWishLists(BaseFiltersRequest filtersRequest)
        {
            var response = new BaseEntityResponse<Wishlist>();
            var routes = GetEntityQuery();

            if (filtersRequest.NumFilter is not null && !string.IsNullOrEmpty(filtersRequest.TextFilter))
            {
                switch (filtersRequest.NumFilter)
                {
                    case 1:
                        routes = routes.Where(x => x.UserId.Equals(filtersRequest.TextFilter));
                        break;
                }
            }

            if (filtersRequest.StateFilter is not null)
            {
                routes = routes.Where(x => x.State.Equals(filtersRequest.StateFilter));
            }

            if (filtersRequest.Sort is null) filtersRequest.Sort = "Id";

            response.TotalRecords = await routes.CountAsync();
            response.Items = await Ordering(filtersRequest, routes, !(bool)filtersRequest.Download!).ToListAsync();

            return response;
        }
    }
}
