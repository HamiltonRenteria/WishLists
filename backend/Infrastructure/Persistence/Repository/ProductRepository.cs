using Domain.Entities;
using Infrastructure.Commons.Bases.Request;
using Infrastructure.Commons.Bases.Response;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(DvpDbContext context) : base(context) { }

        public async Task<BaseEntityResponse<Product>> ListProducts(BaseFiltersRequest request)
        {
            var response = new BaseEntityResponse<Product>();

            var products = request.Id > 0 ? GetEntityQuery().Where(x => x.Id == request.Id) : GetEntityQuery();

            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
            {
                switch (request.NumFilter)
                {
                    case 1:
                        products = products.Where(x => x.Name!.Contains(request.TextFilter));
                        break;
                }
            }

            if (request.StateFilter is not null)
            {
                products = products.Where(x => x.State.Equals(request.StateFilter));
            }

            if (request.Sort is null) request.Sort = "Id";

            response.TotalRecords = await products.CountAsync();
            response.Items = await Ordering(request, products, !(bool)request.Download!).ToListAsync();

            return response;
        }
    }
}
