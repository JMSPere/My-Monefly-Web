namespace MonefyWeb.Application.ModelsWebPage.ViewModels
{
    public class UserLoginViewModel
    {
        public bool Status { get; set; } = false;
        public string Token { get; set; } = "";
        public long UserId { get; set; }
        public long AccountId { get; set; }
    }
}
