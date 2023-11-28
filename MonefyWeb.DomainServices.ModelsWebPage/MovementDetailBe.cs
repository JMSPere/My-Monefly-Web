using MonefyWeb.Transversal.Models;

namespace MonefyWeb.ApplicationServices.ApplicationWebPage.Contracts
{
    public class MovementDetailBe
    {
        public decimal Amount { get; set; }
        public DateTime MovementDate { get; set; }
        public EMovementType Type { get; set; }
        public EPaymentMethod PaymentMethod { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public long CurrencyId { get; set; }
    }
}