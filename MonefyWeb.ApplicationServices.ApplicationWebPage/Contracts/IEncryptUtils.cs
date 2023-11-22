namespace MonefyWeb.ApplicationServices.ApplicationWebPage.Contracts
{
    public interface IEncryptUtils
    {
        string ComputeSHA256Hash(string input);
    }
}
