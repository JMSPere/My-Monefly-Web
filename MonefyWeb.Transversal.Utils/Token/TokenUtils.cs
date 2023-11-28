using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MonefyWeb.Transversal.Utils.Token
{
    public class TokenUtils : ITokenUtils
    {
        private readonly ILogger _logger;
        private readonly ITokenConfiguration _configuration;

        public TokenUtils(ILogger logger, ITokenConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public string DecryptToken(string token, string secret)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(secret);
            using HMACSHA256 hmac = new HMACSHA256(keyBytes);
            byte[] tokenBytes = Encoding.UTF8.GetBytes(token);
            byte[] hashBytes = hmac.ComputeHash(tokenBytes);
            string hashHex = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            _logger.Info("Decrypted token -> " + hashHex);
            Console.WriteLine(hashHex);
            return hashHex;
        }

        public string GetSecret()
        {
            var token = _configuration.Token;
            _logger.Info("Token -> " + token);
            Console.WriteLine(token);
            return token;
        }
    }
}
