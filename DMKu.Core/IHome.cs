using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMKu.Core
{
    public interface IHome
    {
        Task<string> HomeData();
        Task<string> Search();
        Task<string> NewAniList();

        Task<string> AniIndex(string value = null);
    }
}
