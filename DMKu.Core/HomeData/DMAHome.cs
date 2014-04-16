using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMKu.Core
{
    /// <summary>
    /// A站数据
    /// </summary>
    public class DMAHome:IHome
    {
        public Task<string> HomeData()
        {
            throw new NotImplementedException();
        }

        public Task<string> Search()
        {
            throw new NotImplementedException();
        }

        public Task<string> NewAniList()
        {
            throw new NotImplementedException();
        }

        public Task<string> AniIndex(string value = null)
        {
            throw new NotImplementedException();
        }
    }
}
