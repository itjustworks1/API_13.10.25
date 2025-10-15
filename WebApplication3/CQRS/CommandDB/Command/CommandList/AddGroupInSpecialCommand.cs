using Microsoft.EntityFrameworkCore;
using MyMediator.Interfaces;
using MyMediator.Types;
using WebApplication3.CQRS.CommandDB.DTO;
using WebApplication3.DB;

namespace WebApplication3.CQRS.CommandDB.Command.CommandList
{
    public class AddGroupInSpecialCommand : IRequest
    {
        public required int IdSpecial { get; set; }
        public required string Title { get; set; }
        public class AddGroupInSpecialCommandHandler : IRequestHandler<AddGroupInSpecialCommand, Unit>
        {

            private readonly Db131025Context db;
            public AddGroupInSpecialCommandHandler(Db131025Context db)
            {
                this.db = db;
            }

            public async Task<Unit> HandleAsync(AddGroupInSpecialCommand request, CancellationToken ct = default)
            {
                db.Groups.Add(new Group { IdSpecial = request.IdSpecial, Title = request.Title });
                db.SaveChanges();
                return Unit.Value;
            }
        }
    }
}
