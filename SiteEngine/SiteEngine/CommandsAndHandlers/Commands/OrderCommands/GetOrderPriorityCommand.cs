using MediatR;
using Models;

namespace SiteEngine.CommandsAndHandlers.Commands.OrderCommands
{
    public class GetOrderPriorityCommand : IRequest<List<OrderPriority>>
    {
    }
}
