namespace MonefyWeb.Transversal.Utils.Token
{
    public interface ITokenUtils
    {
        string GetSecret();
        string DecryptToken(string token, string secret);
    }
}
