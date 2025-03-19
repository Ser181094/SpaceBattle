using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpaceBattle;
using System.Runtime.InteropServices;

namespace SpaceBattle.GameAgent
{

    [Route("api/[controller]")]
    [ApiController]
    public class RabbitMqController : ControllerBase
    {
        private readonly IRabbitMqService _mqService;

        public RabbitMqController(IRabbitMqService mqService)
        {
            _mqService = mqService;
        }

        [Route("[action]/{message}")]
        [HttpGet]
        public IActionResult SendMessage(string message)
        {
            _mqService.SendMessage(message);

            return Ok("Сообщение отправлено");
        }

        [HttpPost]
        public IActionResult SendMessage(CommandMessage message)
        {
            _mqService.SendMessage(message);

            return Ok("Сообщение отправлено");
        }
    }
}
