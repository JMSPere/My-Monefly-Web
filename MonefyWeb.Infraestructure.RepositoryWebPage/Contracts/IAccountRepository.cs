namespace MonefyWeb.Infraestructure.RepositoryWebPage.Contracts
{
    public interface IAccountRepository
    {
        ChartDataDto GetChartData(long UserId, long AccountId);
    }
}
