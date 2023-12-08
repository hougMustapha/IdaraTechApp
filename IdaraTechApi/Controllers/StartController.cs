using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdaraTechApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StartController : ControllerBase
    {
        [HttpGet("get-starters")]
        public IActionResult Starters()
        {
            return Ok(new JsonResult(new { message = "Seuls les utilisateurs autorisés peuvent voir ce contenu" }));
        }
    }
}
