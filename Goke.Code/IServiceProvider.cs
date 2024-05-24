using System;

public interface IServiceProvider
{
}



namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for getting services from an <see cref="IServiceProvider" />.
    /// </summary>
    public static class ServiceProviderServiceExtensions
    {
        public static T GetRequiredService<T>(this IServiceProvider provider)
        {
            throw new NotImplementedException();
        }

        public static IServiceScope CreateScope(this IServiceProvider provider)
        {
            throw new NotImplementedException();
        }
    }

    public interface IServiceScope : IDisposable
    {
        IServiceProvider ServiceProvider { get; }
    }
}

