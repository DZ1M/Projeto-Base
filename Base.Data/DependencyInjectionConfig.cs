using Base.Data.Repositorys;
using Base.Domain.Interfaces.Repositorys;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Data
{
    public class DependencyInjectionConfig
    {
        public static void Inject(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        }
    }
}
