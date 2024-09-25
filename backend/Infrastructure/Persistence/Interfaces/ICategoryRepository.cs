using Domain.Entities;
using Infrastructure.Commons.Bases.Request;
using Infrastructure.Commons.Bases.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<BaseEntityResponse<Category>> ListCategories(BaseFiltersRequest request);
    }
}
