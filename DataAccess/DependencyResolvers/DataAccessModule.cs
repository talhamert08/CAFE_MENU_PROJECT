using Core.Utilities.IoC;
using DataAccess.Concrete.SQL_EntityFrameWork;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.DependencyResolvers
{
    public class DataAccessModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            //services.AddScoped<IGuidanceTestDal, GuidanceTestDal>();
            services.AddScoped<ICategoryTableDal, CategoryTableDal>();
            services.AddScoped<IProductPropertyTableDal, ProductPropertyTableDal>();
            services.AddScoped<IProductTableDal, ProductTableDal>();
            services.AddScoped<IPropertyTableDal, PropertyTableDal>();
            services.AddScoped<IUserTableDal, UserTableDal>();

        }
    }
}
