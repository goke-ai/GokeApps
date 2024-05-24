#nullable enable

using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace Microsoft.EntityFrameworkCore
{
    public class DbContextOptionsBuilder
    {
    }

    public class DbContextOptionsBuilder<T>
    {
        public DbContextOptionsBuilder()
        {
            
        }

        public DbContextOptions<DbContext>? Options { get; set; }

        public void UseSqlServer(object connectionString)
        {
            throw new NotImplementedException();
        }
    }

    public static class SqlServerDbContextOptionsExtensions
    {
        public static DbContextOptionsBuilder UseSqlServer(this DbContextOptionsBuilder optionsBuilder, 
            string connectionString, Action<SqlServerDbContextOptionsBuilder>? sqlServerOptionsAction = null)
        {
            throw new NotImplementedException();
        }
    }
}