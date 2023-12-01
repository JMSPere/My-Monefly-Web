namespace MonefyWeb.DistributedServices.Models.Models.Users
{
    public class UserLoginResponseDto
    {
        public bool Status { get; set; } = false;
        public string Token { get; set; } = string.Empty;
        public long UserId { get; set; }
        public long AccountId { get; set; }
    }
}
