using Microsoft.AspNetCore.Mvc;
using MyMediator.Types;
using System.Threading.Tasks;
using WebApplication3.CQRS.CommandDB.Command.CommandCount;
using WebApplication3.CQRS.CommandDB.Command.CommandList;
using WebApplication3.CQRS.CommandDB.DTO;

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

    }
}
