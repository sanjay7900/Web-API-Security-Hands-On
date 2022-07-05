using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTwo.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class WebApiTwoController : ControllerBase
    {
       

        

        [HttpGet]
        public ActionResult SecondWebApiCalling()
        {
           return Ok("I m From Web Api Two");
        }
    }
}