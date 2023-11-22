namespace MonefyWeb.DistributedServices.Models.Models.Users
{
    public class UserLoginResponseDto
    {
        public bool Status { get; set; } = false;
        public long UserId { get; set; }
        public long AccountId { get; set; }
    }
}
