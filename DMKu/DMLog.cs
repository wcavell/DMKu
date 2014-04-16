using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DMKu
{
    partial class DMLog
    {
        public static void Trace(string log)
        {
            Debug.WriteLine("DMCore:" + log);
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
