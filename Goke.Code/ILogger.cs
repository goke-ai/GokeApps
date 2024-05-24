using System;

namespace Microsoft.Extensions.Logging
{
    public interface ILogger
    {
    }

    public interface ILogger<out TCategoryName> : ILogger
    {
        void LogError(Exception ex, string v);
    }

}