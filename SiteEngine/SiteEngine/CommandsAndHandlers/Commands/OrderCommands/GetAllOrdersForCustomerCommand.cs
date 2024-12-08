using DtoModelsProj;
using MediatR;

namespace SiteEngine.CommandsAndHandlers.Commands.OrderCommands
{
    public class GetAllOrdersForCustomerCommand : IRequest<ListOrdersForCustomerDto>
    {
        public int UserId { get; set; }
    }
}
