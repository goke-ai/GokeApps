using System;

namespace Microsoft.Data.SqlClient
{
    public class SqlException : Exception
    {
        public int Number { get; set; }
    }
}