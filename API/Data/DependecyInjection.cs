using Core;
using Microsoft.Extensions.DependencyInjection;

namespace Data
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddData(this IServiceCollection services)
        {
            services.AddDbContext<TodoDbContext>(options =>
            {});
            services.AddScoped<ITodoCRUDRepository, TodoCRUDRepository>();

            return services;
        }
    }
}
