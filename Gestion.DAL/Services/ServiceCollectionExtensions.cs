using Gestion.DAL.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Gestion.DAL.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositoryConnector(this IServiceCollection services)
        {
            services.AddTransient<IDatabaseRepository, DatabaseRepository>();
            services.AddTransient<IEventoRepository, EventoRepository>();
            services.AddTransient<IParticipanteRepository, ParticipanteRepository>();
            services.AddTransient<IOrganizadorRepository, OrganizadorRepository>();
            return services;
        }
    }
}