using DtoModelsProj;
using MediatR;

namespace SiteEngine.CommandsAndHandlers.Commands.OrderCommands
{
    public class CreateOrderAndReturnOrderIdCommand : IRequest<ReturnCreatedDtoOrderModel>
    {
        public CreateOrderDto OrderDto { get; set; }
    }
}
