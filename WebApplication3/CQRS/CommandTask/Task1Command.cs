using MyMediator.Interfaces;

namespace WebApplication3.CQRS.CommandTask1
{
    public class Task1Command : IRequest<int>
    {
        public int[] A { get; set; }
        public int K { get; set; }
        public class Task1CommandHandler : IRequestHandler<Task1Command, int>
        {
            public async Task<int> HandleAsync(Task1Command request, CancellationToken ct = default)
            {
                int c = 0;
                foreach (int a in request.A)
                    if (a % request.K == 0)
                        c += a;
                return c;
            }
        }
    }
}
