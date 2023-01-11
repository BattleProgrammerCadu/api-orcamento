
using Microsoft.AspNetCore.Mvc;

namespace api.Controller;

[ApiController]
public class HomeController: ControllerBase
{
    [Route("/")]
    [HttpGet]
    public IActionResult Index()
    {
        return StatusCode(200, new{
            Mensagem = "Fala zé"
        });
    }
}