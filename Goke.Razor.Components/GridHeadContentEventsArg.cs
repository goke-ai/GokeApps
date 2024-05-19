using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goke.Razor.Components
{
    public enum GridHeadContentSearchType { Search, Text, CheckBox, Number, Radio }
    public class GridHeadContentEventsArg
    {
        public string? ColumnName { get; set; }
        public string? Filter { get; set; }
    }
}
