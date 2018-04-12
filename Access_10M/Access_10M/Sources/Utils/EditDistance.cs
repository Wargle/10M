using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Access_10M.Sources.Utils
{
    public class EditDistance
    {
        public static int CalculEditDistance(string original, string test)
        {
            return RecursifMemoization(original, test, new Cache());
        }

        private static int RecursifMemoization(string o, string t, Cache cache)
        {
            int leno = o.Length;
            int lent = t.Length;

            if (cache.Contains(o, t))
                return cache.GetInt(o, t);

            if (Math.Max(leno, lent) <= 2 || Math.Min(leno, lent) <= 1)
            {
                cache.SetInt(o, t, HammingDistance(o, t));
                return cache.GetInt(o, t);
            }
            else
            {
                int d1 = RecursifMemoization(o.Substring(0, leno), t.Substring(0, lent - 1), cache);
                int d2 = RecursifMemoization(o.Substring(0, leno - 1), t.Substring(0, lent), cache);
                int d3 = RecursifMemoization(o.Substring(0, leno - 1), t.Substring(0, lent - 1), cache);

                int firstMin = Math.Min(d1 + 1, d2 + 1);
                if (o.ElementAt(leno - 1) != t.ElementAt(lent - 1))
                {
                    cache.SetInt(o, t, Math.Min(firstMin, d3 + 1));
                    return cache.GetInt(o, t);
                }
                else
                {
                    cache.SetInt(o, t, Math.Min(firstMin, d3));
                    return cache.GetInt(o, t);
                }
            }
        }

        private static int HammingDistance(string o, string t)
        {
            int leno = o.Length;
            int lent = t.Length;

            int d = Math.Abs(leno - lent);

            for (int i = 0; i < Math.Min(leno, lent); i++)
            {
                if (o.ElementAt(i) != t.ElementAt(i))
                    d++;
            }
            return d;
        }

        private class Cache
        {
            private Dictionary<string, Dictionary<string, int>> map;

            public Cache() { map = new Dictionary<string, Dictionary<string, int>>(); }

            public bool Contains(string m1, string m2)
            {
                if (map.ContainsKey(m1))
                {
                    return map[m1].ContainsKey(m2);
                }
                else if (map.ContainsKey(m2))
                {
                    return map[m2].ContainsKey(m1);
                }
                else
                {
                    return false;
                }
            }

            public int GetInt(string m1, string m2)
            {
                if (map.ContainsKey(m1))
                {
                    return map[m1][m2];
                }
                else
                {
                    return map[m2][m1];
                }
            }

            public void SetInt(string m1, string m2, int val)
            {
                if (map.ContainsKey(m1))
                {
                    if (!map[m1].ContainsKey(m2))
                    {
                        Dictionary<string, int> hm = new Dictionary<string, int>();
                        map[m1] = hm;
                        hm[m2] = val;
                    }
                }
                else if (map.ContainsKey(m2))
                {
                    if (!map[m2].ContainsKey(m1))
                    {
                        Dictionary<string, int> hm = new Dictionary<string, int>();
                        map[m2] = hm;
                        hm[m1] = val;
                    }
                }
                else
                {
                    Dictionary<string, int> hm = new Dictionary<string, int>();
                    map[m1] = hm;
                    hm[m2] = val;
                }
            }
        }
    }
}
