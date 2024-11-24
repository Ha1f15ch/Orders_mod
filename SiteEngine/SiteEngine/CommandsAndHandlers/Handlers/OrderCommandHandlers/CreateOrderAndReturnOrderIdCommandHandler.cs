using DtoModelsProj;
using MediatR;
using Repositories.InterfaceRepositories;
using SiteEngine.CommandsAndHandlers.Commands.OrderCommands;

namespace SiteEngine.CommandsAndHandlers.Handlers.OrderCommandHandlers
{
    public class CreateOrderAndReturnOrderIdCommandHandler : IRequestHandler<CreateOrderAndReturnOrderIdCommand, ReturnCreatedDtoOrderModel>
    {
        private readonly IOrderRepository orderRepository;

        public CreateOrderAndReturnOrderIdCommandHandler(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
            Console.WriteLine($"Обработчик CreateOrderAndReturnOrderIdCommandHandler создан");
        }

        public async Task<ReturnCreatedDtoOrderModel> Handle(CreateOrderAndReturnOrderIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await orderRepository.CreateNewOrder(request.OrderDto);
            }
            catch ( Exception ex )
            {
                Console.WriteLine($"Возникла ошибка при обработке команды по созданию записи Order - {ex.Message}");
                return new ReturnCreatedDtoOrderModel
                {
                    IsCreated = false,
                    OrderId = 0
                };
            }
        }
    }
}
