using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moo
{
    class Class1
    {
        public List<double> square(List<double> a, List<double> b)
        {
            List<double> c = new List<double>();
            for (int i = 0; i < a.Count; i++)
            {
                if (a.Count == b.Count)
                {


                    double sq = a[i] * b[i];
                    c.Add(sq);
                }
            }
            return c;
        }
    }
}
