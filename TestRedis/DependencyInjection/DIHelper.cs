using TestRedis.Agents;
using TestRedis.Caches;
using TestRedis.Services;

namespace TestRedis.DependencyInjection
{
    public static class DIHelper
    {
        public static IServiceCollection AddAutoFacProjectDependencies(this IServiceCollection services)
        {
            // Register services here
            services.AddScoped<IUserAgent, UserAgent>();
            services.AddScoped<IUserCache, UserCache>();
            services.AddScoped<IUserService, UserService>();
            //services.AddSingleton<IMySingletonService, MySingletonService>();
            //services.AddTransient<IMyTransientService, MyTransientService>();
            return services;
        }

    }
}
