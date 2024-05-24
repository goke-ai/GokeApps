namespace Microsoft.Extensions.Configuration
{
    public interface IConfiguration
    {
        string GetConnectionString(string v);
    }
}