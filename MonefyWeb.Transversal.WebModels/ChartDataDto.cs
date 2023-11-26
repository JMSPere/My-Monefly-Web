namespace MonefyWeb.ApplicationServices.ApplicationWebPage.Implementations
{
    public class ChartDataDto
    {
        public decimal TotalIncomes { get; set; }
        public List<KeyValuePair<long, string>> IncomeCategories { get; set; }
        public decimal TotalExpenses { get; set; }
        public List<KeyValuePair<long, string>> ExpenseCategories { get; set; }
        public decimal Balance { get; set; }

        public List<object> ChartData { get; set; }
        public List<KeyValuePair<string, string>> Categories { get; set; }
        public List<KeyValuePair<string, string>> CartDataColor { get; set; }
    }
}