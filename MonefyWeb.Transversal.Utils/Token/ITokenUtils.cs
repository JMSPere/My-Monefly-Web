using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonefyWeb.Transversal.Utils.Token
{
    public interface ITokenUtils
    {
        string GetSecret();
        string DecryptToken(string token, string secret);
    }
}
