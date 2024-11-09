using Business.Services;
using Core.DataAccess;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Business.DependencyResolvers
{
    public class BusinessModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            //services.AddScoped<IGuidanceQuestionService, GuidanceQuestionManager>();
            services.AddScoped<ICategoryTableService, CategoryTableManager>();
            services.AddScoped<IProductPropertyTableService, ProductPropertyTableManager>();
            services.AddScoped<IProductTableService, ProductTableManager>();
            services.AddScoped<IPropertyTableService, PropertyTableManager>();
            services.AddScoped<IUserTableService, UserTableManager>();

        }
    }
}
