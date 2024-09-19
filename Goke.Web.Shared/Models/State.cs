using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goke.Web.Shared.Models
{
    public class State
    {
        readonly Dictionary<string, object?>? keyValues = [];

        public object? this[string s]
        {
            get
            {
                return keyValues!.TryGetValue(s, out object? value) ? value : default;
            }
            set
            {
                if (s is not null)
                {
                    //keyValues!.Remove(s);
                    keyValues![s] = value;
                }
            }
        }

        public object? Get(string key)
        {
            return keyValues!.TryGetValue(key, out object? value) ? value : default;
        }

        public void Set(string key, object value)
        {
            if (key is not null)
            {
                keyValues!.Remove(key);
                keyValues!.Add(key, value);
            }
        }
    }

}
