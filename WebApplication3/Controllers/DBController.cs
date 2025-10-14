using Microsoft.AspNetCore.Mvc;
using MyMediator.Types;
using WebApplication3.CQRS.CommandDB.Command;
using WebApplication3.CQRS.CommandDB.Command.CommandCount;
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
        public async Task<ActionResult<int>> GetCountGenderByIdGroup(int idGroup)
        {
            var command = new GetCountGenderByIdGroupCommand() { IdGroup = idGroup };
            var result = await mediator.SendAsync(command);
            return Ok(result);
        }

        [HttpPost("GetListStudentNotInGroup")]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetListStudentNotInGroup()
        {
            var command = new GetListStudentNotInGroupCommand();
            var result = await mediator.SendAsync(command);
            return Ok(result);
        }

        [HttpPost("GetListGroupNotHaveStudent")]
        public async Task<ActionResult<IEnumerable<GroupDTO>>> GetListGroupNotHaveStudent()
        {
            var command = new GetListGroupNotHaveStudentCommand() {};
            var result = await mediator.SendAsync(command);
            return Ok(result);
        }

        //[HttpPost("GetListGroupAndStudentInGroup")]
        //public async Task<ActionResult<int>> GetListGroupAndStudentInGroup()
        //{
        //    var command = new GetListGroupAndStudentInGroupCommand() {};
        //    var result = await mediator.SendAsync(command);
        //    return Ok(result);
        //}

        //[HttpPost("GetListGroupAndStudentInGroupByIdSpecial")]
        //public async Task<ActionResult<int>> GetListGroupAndStudentInGroupByIdSpecial()
        //{
        //    var command = new GetListGroupAndStudentInGroupByIdSpecialCommand() {};
        //    var result = await mediator.SendAsync(command);
        //    return Ok(result);
        //}

    }
}
