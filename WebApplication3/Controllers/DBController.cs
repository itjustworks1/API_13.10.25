using Microsoft.AspNetCore.Mvc;
using MyMediator.Types;
using WebApplication3.CQRS.CommandDB.Command.CommandList;
using WebApplication3.CQRS.CommandDB.DTO;
using WebApplication3.CQRS.CommandTask1;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DBController : ControllerBase
    {
        private readonly Mediator mediator;
        public DBController(MyMediator.Types.Mediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("GetListStudentByIdGroup")]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetListStudentByIdGroup(int idGroup)
        {
            var command = new GetListStudentByIdGroupCommand() { IdGroup = idGroup };
            var result = await mediator.SendAsync(command);
            return Ok(result);
        }

        [HttpPost("GetCountGenderByIdGroup")]
        public async Task<ActionResult<int>> GetCountGenderByIdGroup(int k, [FromBody] int[] A)
        {
            var command = new Task1Command() { A = A, K = k };
            var result = await mediator.SendAsync(command);
            return Ok(result);
        }

        [HttpPost("GetListStudentNotInGroup")]
        public async Task<ActionResult<int>> GetListStudentNotInGroup(int k, [FromBody] int[] A)
        {
            var command = new Task1Command() { A = A, K = k };
            var result = await mediator.SendAsync(command);
            return Ok(result);
        }

        [HttpPost("GetListGroupNotHaveStudent")]
        public async Task<ActionResult<int>> GetListGroupNotHaveStudent(int k, [FromBody] int[] A)
        {
            var command = new Task1Command() { A = A, K = k };
            var result = await mediator.SendAsync(command);
            return Ok(result);
        }

        [HttpPost("GetListGroupAndStudentInGroup")]
        public async Task<ActionResult<int>> GetListGroupAndStudentInGroup(int k, [FromBody] int[] A)
        {
            var command = new Task1Command() { A = A, K = k };
            var result = await mediator.SendAsync(command);
            return Ok(result);
        }

        [HttpPost("GetListGroupAndStudentInGroupByIdSpecial")]
        public async Task<ActionResult<int>> GetListGroupAndStudentInGroupByIdSpecial(int k, [FromBody] int[] A)
        {
            var command = new Task1Command() { A = A, K = k };
            var result = await mediator.SendAsync(command);
            return Ok(result);
        }

    }
}
