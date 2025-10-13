using Microsoft.AspNetCore.Mvc;
using MyMediator.Types;
using System.Threading.Tasks;
using WebApplication3.CQRS.CommandDB.Command.CommandCount;
using WebApplication3.CQRS.CommandDB.Command.CommandList;
using WebApplication3.CQRS.CommandDB.DTO;
using WebApplication3.CQRS.CommandTask1;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArrayController : ControllerBase
    {
        private readonly Mediator mediator;
        public ArrayController(MyMediator.Types.Mediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpPost("Task1/{k}")]
        public async Task<ActionResult<int>> Task1(int k, [FromBody] int[] A)
        {
            var command = new Task1Command() { A = A, K = k }; 
            var result = await mediator.SendAsync(command);
            return Ok(result);
        }
    }
}
