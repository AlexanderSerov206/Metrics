using MetricsAgent.Models.Requests;
using MetricsAgent.Models;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {

        private readonly ILogger<RamMetricsController> _logger;
        private readonly IRamMetricsRepository _RamMetricsRepository;

        public RamMetricsController(IRamMetricsRepository RamMetricsRepository,
            ILogger<RamMetricsController> logger)
        {
            _RamMetricsRepository = RamMetricsRepository;
            _logger = logger;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] RamMetricCreateRequest request)
        {
            _logger.LogInformation("Create Ram metric.");
            _RamMetricsRepository.Create(new Models.RamMetric
            {
                Value = request.Value,
                Time = (long)request.Time.TotalSeconds
            });
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        //[Route("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<RamMetric>> GetRamMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get Ram metrics call.");
            return Ok(_RamMetricsRepository.GetByTimePeriod(fromTime, toTime));
        }

        [HttpGet("available/from/{fromTime}/to/{toTime}")]
        public IActionResult GetAvailableRamMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }
    }
}
