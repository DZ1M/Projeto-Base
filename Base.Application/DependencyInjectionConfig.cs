using Microsoft.Extensions.DependencyInjection;

namespace Base.Application
{
    public class DependencyInjectionConfig
    {
        public static void Inject(IServiceCollection services)
        {
            services.AddTransient<Domain.Interfaces.Application.IUsuarioService, Services.UsuarioService>();
        }
    }
}
