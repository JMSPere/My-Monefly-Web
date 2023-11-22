using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonefyWeb.ApplicationServices.ApplicationWebPage.Contracts
{
    public interface IEncryptUtils
    {
        string ComputeSHA256Hash(string input);
    }
}
