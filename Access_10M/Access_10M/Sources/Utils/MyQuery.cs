using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Access_10M.Sources.Utils
{
    public class MyQuery
    {
        public string Request { get; private set; }
        public Dictionary<string, object> Vals { get; private set; }
        
        public MyQuery(string r, Dictionary<string, object> v = null)
        {
            Request = r;
            Vals = v;
        }
    }
}
