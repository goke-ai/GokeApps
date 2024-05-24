using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public interface IServiceCollection
    {
        void AddDbContext<T>(Func<object, object> value);
        void AddTransient<T>();
        void AddTransient<T1, T2>();
    }
}

public static class EntityFrameworkServiceCollectionExtensions
{
    public static IServiceCollection AddDbContext<TContext>(
        this IServiceCollection serviceCollection,
        Action<DbContextOptionsBuilder>? optionsAction = null,
        ServiceLifetime contextLifetime = ServiceLifetime.Scoped,
        ServiceLifetime optionsLifetime = ServiceLifetime.Scoped)
        where TContext : DbContext
    {
        throw new NotImplementedException();
    }
}
    