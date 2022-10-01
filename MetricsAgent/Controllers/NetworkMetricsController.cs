using MetricsAgent.Models.Requests;
using MetricsAgent.Models;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {

        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly INetworkMetricsRepository _NetworkMetricsRepository;

        public NetworkMetricsController(INetworkMetricsRepository NetworkMetricsRepository,
            ILogger<NetworkMetricsController> logger)
        {
            _NetworkMetricsRepository = NetworkMetricsRepository;
            _logger = logger;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] NetworkMetricCreateRequest request)
        {
            _logger.LogInformation("Create network metric.");
            _NetworkMetricsRepository.Create(new Models.NetworkMetric
            {
                Value = request.Value,
                Time = (long)request.Time.TotalSeconds
            });
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        //[Route("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<NetworkMetric>> GetNetworkMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get network metrics call.");
            return Ok(_NetworkMetricsRepository.GetByTimePeriod(fromTime, toTime));
        }
    }
}
