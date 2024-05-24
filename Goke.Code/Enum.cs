using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    //
    // Summary:
    //     Provides the base class for enumerations.
    public abstract class Enum //: ValueType, IComparable, IConvertible, IFormattable
    {
        public static TEnum[] GetValues<TEnum>() // where TEnum : struct, Enum
        {
            throw new NotImplementedException();
        }
    }
}
