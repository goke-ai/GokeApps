using System;
using System.Configuration;

namespace Microsoft.Extensions.Configuration
{
    public interface IConfigurationBuilder
    {
        IConfigurationBuilder SetBasePath(string v);
        IConfigurationBuilder AddJsonFile(string v);
        IConfiguration Build();
    }

    public class ConfigurationBuilder : IConfigurationBuilder
    {
        public ConfigurationBuilder()
        {
        }

        public IConfigurationBuilder SetBasePath(string v)
        {
            throw new NotImplementedException();
        }
        public IConfigurationBuilder AddJsonFile(string v)
        {
            throw new NotImplementedException();
        }

        IConfiguration IConfigurationBuilder.Build()
        {
            throw new NotImplementedException();
        }
    }

}