using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace DMKu.Core
{
    internal class DMLog
    {
        public static void Trace(string log) 
        {
            Debug.WriteLine("DMCore:"+log);
        }

        public static void Trace(object log)
        {
            Debug.WriteLine((object)("DMCore:" + log)); 
        }
        public static void Trace(string value, string category)
        {
            Debug.WriteLine(value, category);
        }
        public static void Trace(object value, string category)
        {
            Debug.WriteLine(value, category);
        }
    }
}
