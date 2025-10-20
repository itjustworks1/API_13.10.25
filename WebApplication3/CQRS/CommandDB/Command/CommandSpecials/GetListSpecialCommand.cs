using MyMediator.Interfaces;
using WebApplication3.CQRS.CommandDB.Command.CommandList;
using WebApplication3.CQRS.CommandDB.DTO;
using WebApplication3.DB;

namespace WebApplication3.CQRS.CommandDB.Command.CommandSpecials
{
    public class GetListSpecialCommand : IRequest<IEnumerable<SpecialDTO>>
    {
        public class GetListSpecialCommandHandler : IRequestHandler<GetListSpecialCommand, IEnumerable<SpecialDTO>>
        {

            private readonly Db131025Context db;
            public GetListSpecialCommandHandler(Db131025Context db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<SpecialDTO>> HandleAsync(GetListSpecialCommand request, CancellationToken ct = default)
            {
                return db.Specials.Select(s => new SpecialDTO { Id = s.Id, Title = s.Title });
            }
        }
    }
}
