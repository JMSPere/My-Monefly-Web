using MonefyWeb.Transversal.Models;

namespace MonefyWeb.Infraestructure.RepositoryWebPage.Contracts
{
    public class MovementDetailDto
    {
        public decimal Amount { get; set; }
        public DateTime MovementDate { get; set; }
        public string Concept { get; set; }

        public EMovementType Type { get; set; }
        public EPaymentMethod PaymentMethod { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public long CurrencyId { get; set; }
    }
}