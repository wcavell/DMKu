using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMKu.Core
{
    public class DMKuData
    {
        /// <summary>
        /// 数据类型
        /// A、B、C
        /// </summary>
        public string Type { get; set; }
        public Dictionary<string, List<DMVideoInfo>> Date { get; set; }
    }
}
