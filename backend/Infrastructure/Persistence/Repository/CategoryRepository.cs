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
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DvpDbContext context) : base(context) { }

        public async Task<BaseEntityResponse<Category>> ListCategories(BaseFiltersRequest request)
        {
            var response = new BaseEntityResponse<Category>();
            var categories = GetEntityQuery();

            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
            {
                switch (request.NumFilter)
                {
                    case 1:
                        categories = categories.Where(x => x.Name!.Contains(request.TextFilter));
                        break;
                }
            }

            if (request.StateFilter is not null)
            {
                categories = categories.Where(x => x.State.Equals(request.StateFilter));
            }

            if (request.Sort is null) request.Sort = "Id";

            response.TotalRecords = await categories.CountAsync();
            response.Items = await Ordering(request, categories, !(bool)request.Download!).ToListAsync();

            return response;
        }
    }
}
