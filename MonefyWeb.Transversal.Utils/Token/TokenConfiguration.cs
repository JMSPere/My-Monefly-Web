using Microsoft.Extensions.Configuration;

namespace MonefyWeb.Transversal.Utils.Token
{
    public class TokenConfiguration : ITokenConfiguration
    {
        public string Token => "MonefyWebDB";
    }
}