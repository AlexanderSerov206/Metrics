using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotnetMetricsController : ControllerBase
    {

        private readonly ILogger<DotnetMetricsController> _logger;
        private readonly IDotnetMetricsRepository _dotnetMetricsRepository;

        public DotnetMetricsController(IDotnetMetricsRepository dotnetMetricsRepository,
            ILogger<DotnetMetricsController> logger)
        {
            _dotnetMetricsRepository = dotnetMetricsRepository;
            _logger = logger;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] DotnetMetricCreateRequest request)
        {
            _logger.LogInformation("Create cpu metric.");
            _dotnetMetricsRepository.Create(new Models.DotnetMetric
            {
                Value = request.Value,
                Time = (long)request.Time.TotalSeconds
            });
            return Ok();
        }

        [HttpGet("errors-count/from/{fromTime}/to/{toTime}")]
        public IActionResult GetErrorsCountMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }
    }
}
