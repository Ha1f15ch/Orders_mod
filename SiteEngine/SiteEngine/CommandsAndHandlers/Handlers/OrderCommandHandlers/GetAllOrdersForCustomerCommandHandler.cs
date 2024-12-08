using DtoModelsProj;
using MediatR;
using Repositories.InterfaceRepositories;
using SiteEngine.CommandsAndHandlers.Commands.OrderCommands;
using SiteEngine.Models.CustomerUserProfileModels;

namespace SiteEngine.CommandsAndHandlers.Handlers.OrderCommandHandlers
{
    public class GetAllOrdersForCustomerCommandHandler : IRequestHandler<GetAllOrdersForCustomerCommand, ListOrdersForCustomerDto>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderStatusRepository orderStatusRepository;
        private readonly IOrderPriorityRepository orderPriorityRepository;
        private readonly IUserAccauntRepository userAccauntRepository;
        private readonly IUserProfileRepository userProfileRepository;
        private readonly ICustomerUserProfileRepository customerUserProfileRepository;
        private readonly IEmployerUserProfileRepository employerUserProfileRepository;

        public GetAllOrdersForCustomerCommandHandler(IOrderRepository orderRepository,
                                                     IOrderStatusRepository orderStatusRepository,
                                                     IOrderPriorityRepository orderPriorityRepository,
                                                     IUserAccauntRepository userAccauntRepository,
                                                     IUserProfileRepository userProfileRepository,
                                                     ICustomerUserProfileRepository customerUserProfileRepository,
                                                     IEmployerUserProfileRepository employerUserProfileRepository)
        {
            this.orderRepository = orderRepository;
            this.orderStatusRepository = orderStatusRepository;
            this.orderPriorityRepository = orderPriorityRepository;
            this.customerUserProfileRepository = customerUserProfileRepository;
            this.userProfileRepository = userProfileRepository;
            this.employerUserProfileRepository = employerUserProfileRepository;
            this.userAccauntRepository = userAccauntRepository;

            Console.WriteLine($"Обработчик GetAllOrdersForCustomerCommandHandler создан");
        }
         
        public async Task<ListOrdersForCustomerDto> Handle(GetAllOrdersForCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.UserId > 0) 
                {
                    return new ListOrdersForCustomerDto
                    {
                        ListOrders = await orderRepository.GetAllOrders(),
                        UserProfile = await userProfileRepository.GetUserProfileByUserId(request.UserId),
                        CustomerProfile = await customerUserProfileRepository.GetCustomerUserProfileByUserId(request.UserId),
                        ListOrderPriorities = await orderPriorityRepository.GetAllPriority(),
                        ListOrderStatuses = await orderStatusRepository.GetAllOrderStatuses(),
                        HasError = false,
                        ErrorMessage = string.Empty
                    };
                }

                throw new ArgumentException($"Передано некорректное значение userId = {request.UserId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"При выполнении команды по предоставлению данных о заказах пользователя Customer, возникла ошибка - {ex.Message}");
                return new ListOrdersForCustomerDto
                {
                    CustomerProfile = await customerUserProfileRepository.GetCustomerUserProfileByUserId(request.UserId),
                    ListOrderPriorities = await orderPriorityRepository.GetAllPriority(),
                    ListOrderStatuses = await orderStatusRepository.GetAllOrderStatuses(),
                    HasError = true,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
    
}
