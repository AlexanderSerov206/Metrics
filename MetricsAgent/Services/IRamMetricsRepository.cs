using MetricsAgent.Models;

namespace MetricsAgent.Services
{
    public interface IRamMetricsRepository : IRepository<RamMetric>
    {
        IList<RamMetric> GetAvailableRamMetrics(TimeSpan timeFrom, TimeSpan timeTo);
    }
}
