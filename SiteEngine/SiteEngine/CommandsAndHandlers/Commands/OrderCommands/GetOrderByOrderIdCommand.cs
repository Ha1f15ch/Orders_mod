using DtoModelsProj;
using MediatR;
using Models;

namespace SiteEngine.CommandsAndHandlers.Commands.OrderCommands
{
    public class GetOrderByOrderIdCommand : IRequest<OrderDataInformation_Dto>
    {
        public int OrderId { get; set; }
        public int Userid { get; set; }
    }
}
