using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace WebApiOneSecurityExample.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class WeatherForecastController : ControllerBase
    {

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            return Ok(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray());

        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Hello()
        {
            return Ok("Hii I m Here ...AllowAnonymous");
        }
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public ActionResult IMAdmin()
        {
            return Ok("I M called From Admin Side It Means I  m a Admin ...");
        }
        [HttpGet]
        [Authorize(Roles ="Admin,Viewer")]
       

        public ActionResult MayAdminOrViewer()
        {
            return Ok("I m from bath Side Admin Or Viewer ");
        }
        [HttpGet]
        [Authorize(Policy = "RequireMultipleRole")]
        public ActionResult ImAdminAndViewer()
        {
            return Ok("I m  Admin Adn Also  Viewer");
        }
        [HttpGet]
        [Authorize(Policy = "AgeOver18")]
        public ActionResult ImOlder()
        {

            return Ok("Yes i m 18 Years Older  And My Qualification B.tech");
        }

    }
}