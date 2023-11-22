namespace MonefyWeb.Application.WebPage.Models
{
    public class TemporalHelperModel
    {
        public decimal IncomeAmount { get; set; }
        public int IncomeId { get; set; }
        public string Concept { get; set; } = "";
        public int IncomeCategory { get; set; }
        public decimal ExpenseAmount { get; set; }
        public int ExpenseId { get; set; }
        public int ExpenseCategory { get; set; }
    }
}
