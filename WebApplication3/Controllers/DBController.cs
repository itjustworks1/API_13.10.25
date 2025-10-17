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

        /// <summary>
        /// Получить список студентов из группы по указанному индексу группы
        /// </summary>
        /// <param name="idGroup"></param>
        /// <returns></returns>
        [HttpPost("GetListStudentByIdGroup")]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetListStudentByIdGroup(int idGroup)
        {
            var command = new GetListStudentByIdGroupCommand() { IdGroup = idGroup };
            var result = await mediator.SendAsync(command);
            return Ok(result);
        }

        /// <summary>
        /// Получить информацию о кол-ве мальчиков и девочек в группе по указанному индексу группы
        /// </summary>
        /// <param name="idGroup"></param>
        /// <returns></returns>
        [HttpPost("GetCountGenderByIdGroup")]
        public async Task<ActionResult<int>> GetCountGenderByIdGroup(int idGroup)
        {
            var command = new GetCountGenderByIdGroupCommand() { IdGroup = idGroup };
            var result = await mediator.SendAsync(command);
            return Ok(result);
        }

        /// <summary>
        /// Получить список студентов, которые не привязаны ни к одной группе
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetListStudentNotInGroup")]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetListStudentNotInGroup()
        {
            var command = new GetListStudentNotInGroupCommand();
            var result = await mediator.SendAsync(command);
            return Ok(result);
        }

        /// <summary>
        /// Получить список групп, в которых нет студентов
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetListGroupNotHaveStudent")]
        public async Task<ActionResult<IEnumerable<GroupDTO>>> GetListGroupNotHaveStudent()
        {
            var command = new GetListGroupNotHaveStudentCommand() {};
            var result = await mediator.SendAsync(command);
            return Ok(result);
        }

        /// <summary>
        /// Получить общую статистику по отделению - список групп с указанием кол-ва студентов в каждой группе
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetListGroupAndStudentInGroup")]
        public async Task<ActionResult<IEnumerable<GroupDTO>>> GetListGroupAndStudentInGroup()
        {
            var command = new GetListGroupAndStudentInGroupCommand() { };
            var result = await mediator.SendAsync(command);
            return Ok(result);
        }

        /// <summary>
        /// Такая же задача, как п.5, но с указанием индекса конкретной специальности
        /// </summary>
        /// <param name="idSpecial"></param>
        /// <returns></returns>
        [HttpPost("GetListGroupAndStudentInGroupByIdSpecial")]
        public async Task<ActionResult<IEnumerable<GroupDTO>>> GetListGroupAndStudentInGroupByIdSpecial(int idSpecial)
        {
            var command = new GetListGroupAndStudentInGroupByIdSpecialCommand() { IdSpecial = idSpecial };
            var result = await mediator.SendAsync(command);
            return Ok(result);
        }

        /// <summary>
        /// Добавление новой группы для указанной специальности
        /// </summary>
        /// <param name="idSpecial"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        [HttpPost("AddGroupInSpecial")]
        public async Task AddGroupInSpecial(int idSpecial, string title)
        {
            var command = new AddGroupInSpecialCommand() { IdSpecial = idSpecial,  Title = title };
            await mediator.SendAsync(command);
            return;
        }

        /// <summary>
        /// Перевод указанного студента в указанную группу
        /// </summary>
        /// <param name="idStudent"></param>
        /// <param name="idGroup"></param>
        /// <returns></returns>
        [HttpPost("TransferStudentToGroup")]
        public async Task TransferStudentToGroup(int idStudent, int idGroup)
        {
            var command = new TransferStudentToGroupCommand() { IdStudent = idStudent, IdGroup = idGroup };
            await mediator.SendAsync(command);
            return;
        }

        /// <summary>
        /// Добавить метод, который возвратит дублирующегося студента (один и тот же человек в двух разных группах)
        /// </summary>
        /// <returns></returns>
        [HttpPost("ReturnDuplicateStudent")]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> ReturnDuplicateStudent()
        {
            var command = new ReturnDuplicateStudentCommand();
            var result = await mediator.SendAsync(command);
            return Ok(result);
        }

    }
}
