using E_CommerceCore.Core.Domain.Entities;
using E_CommerceCore.Core.Domain.Repositories.EntityFrameworkCore;
using E_CommerceCore.Core.Domain.Repositories.Interfaces;
using E_CommerceCore.EntityFrameworkCore.Configuration;

namespace E_CommerceCore.EntityFrameworkCore.Repositories.EntityFrameworkCore
{
    public class EfCoreCategoryRepository : EfCoreRepository<Category, ECommerceDbContext>, IEfCoreCategoryRepository
    {
        public EfCoreCategoryRepository(ECommerceDbContext context) : base(context)
        {

        }
    }
}