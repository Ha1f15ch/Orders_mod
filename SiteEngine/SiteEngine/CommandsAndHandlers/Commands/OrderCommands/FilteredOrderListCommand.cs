using DtoModelsProj;
using MediatR;

namespace SiteEngine.CommandsAndHandlers.Commands.OrderCommands
{
    public class FilteredOrderListCommand : IRequest<ListOrdersForCustomerDto>
    {
        public int UserId { get; set; }
        public DateOnly startCreateD {  get; set; } = DateOnly.MinValue;
        public DateOnly endCreateD {  get; set; } = DateOnly.MinValue;
        public DateOnly startDeleteD { get; set; } = DateOnly.MinValue;
        public DateOnly endDeleteD { get; set; } = DateOnly.MinValue;
        public string? listStatus { get; set; }
        public string? listPriority { get; set; }
        public bool isCustomer { get; set; }
    }
}
