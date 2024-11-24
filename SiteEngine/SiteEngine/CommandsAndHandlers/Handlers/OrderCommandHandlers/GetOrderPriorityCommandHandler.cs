using MediatR;
using Models;
using Repositories.InterfaceRepositories;
using SiteEngine.CommandsAndHandlers.Commands.OrderCommands;

namespace SiteEngine.CommandsAndHandlers.Handlers.OrderCommandHandlers
{
    public class GetOrderPriorityCommandHandler : IRequestHandler<GetOrderPriorityCommand, List<OrderPriority>>
    {
        private readonly IOrderPriorityRepository orderPriorityRepository;

        public GetOrderPriorityCommandHandler(IOrderPriorityRepository orderPriorityRepository)
        {
            this.orderPriorityRepository = orderPriorityRepository;
            Console.WriteLine($"Обработчик GetOrderPriorityCommandHandler создан");
        }

        public async Task<List<OrderPriority>> Handle(GetOrderPriorityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await orderPriorityRepository.GetAllPriority();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"При выполнении команды по возврату списка приоритетов для заказов, возникла ошибка - {ex.Message}");
                return new List<OrderPriority>();
            }
        }
    }
}
