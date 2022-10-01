using MetricsAgent.Models;

namespace MetricsAgent.Services
{
    public interface IDotnetMetricsRepository : IRepository<DotnetMetric>
    {
        int GetErrorsCountByPeriod(TimeSpan timeFrom, TimeSpan timeTo);
    }
}
