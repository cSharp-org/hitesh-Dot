using System.Web.Http;
using Dummy.Services;

namespace Dummy.Controllers
{
    public class CalculatorController : ApiController
    {
        private readonly ICalculatorService _calculatorService;

        public CalculatorController()
        {
            // In a real application, use dependency injection
            _calculatorService = new CalculatorService(new FileLoggingService());
        }

        // GET: api/Calculator/Add?a=1&b=2
        [HttpGet]
        [Route("api/Calculator/Add")]
        public IHttpActionResult Add(int a, int b)
        {
            var result = _calculatorService.Add(a, b);
            return Ok(result);
        }

        // GET: api/Calculator/Concat?a=hello&b=123
        [HttpGet]
        [Route("api/Calculator/Concat")]
        public IHttpActionResult Concat(string a, int b)
        {
            var result = _calculatorService.Concat(a, b);
            return Ok(result);
        }

        // GET: api/Calculator/MultiplyAndAdd?a=2.5&b=3
        [HttpGet]
        [Route("api/Calculator/MultiplyAndAdd")]
        public IHttpActionResult MultiplyAndAdd(double a, int b)
        {
            var result = _calculatorService.MultiplyAndAdd(a, b);
            return Ok(result);
        }

        // GET: api/Calculator/RepeatString?s=hi&times=3
        [HttpGet]
        [Route("api/Calculator/RepeatString")]
        public IHttpActionResult RepeatString(string s, int times)
        {
            var result = _calculatorService.RepeatString(s, times);
            return Ok(result);
        }
    }
} 