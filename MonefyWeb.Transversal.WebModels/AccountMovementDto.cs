using MonefyWeb.Transversal.Models;

namespace MonefyWeb.DistributedServices.Models.Models.Accounts
{
    public class AccountMovementDto
    {
        public string Concept { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public EMovementType Type { get; set; }
        public EPaymentMethod PaymentMethod { get; set; }
        public long CategoryId { get; set; }
    }
}
