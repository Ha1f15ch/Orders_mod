using DtoModelsProj;
using MediatR;
using Repositories.InterfaceRepositories;
using SiteEngine.CommandsAndHandlers.Commands.OrderCommands;
using System.Net.WebSockets;

namespace SiteEngine.CommandsAndHandlers.Handlers.OrderCommandHandlers
{
    public class FilteredOrderListCommandHandler : IRequestHandler<FilteredOrderListCommand, ListOrdersForCustomerDto>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderStatusRepository orderStatusRepository;
        private readonly IOrderPriorityRepository orderPriorityRepository;
        private readonly IUserAccauntRepository userAccauntRepository;
        private readonly IUserProfileRepository userProfileRepository;
        private readonly ICustomerUserProfileRepository customerUserProfileRepository;
        private readonly IEmployerUserProfileRepository employerUserProfileRepository;

        public FilteredOrderListCommandHandler(IOrderRepository orderRepository,
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
            this.userAccauntRepository = userAccauntRepository;
            this.userProfileRepository = userProfileRepository;
            this.customerUserProfileRepository = customerUserProfileRepository;
            this.employerUserProfileRepository = employerUserProfileRepository;

            Console.WriteLine("Обработчик FilteredOrderListCommandHandler создан");
        }

        public async Task<ListOrdersForCustomerDto> Handle(FilteredOrderListCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if(request.UserId > 0)
                {
                    var user = await userAccauntRepository.GetUserAccauntByUserId(request.UserId);

                    if(user != null)
                    {
                        var resultListOrders = await orderRepository.GetFilteredListOrders(request.startCreateD, request.endCreateD, request.startDeleteD, request.endDeleteD, request.listStatus, request.listPriority, request.isCustomer, user.Id);

                        return new ListOrdersForCustomerDto
                        {
                            ListOrders = resultListOrders,
                            UserProfile = await userProfileRepository.GetUserProfileByUserId(user.Id),
                            CustomerProfile = await customerUserProfileRepository.GetCustomerUserProfileByUserId(user.Id),
                            ListOrderPriorities = await orderPriorityRepository.GetAllPriority(),
                            ListOrderStatuses = await orderStatusRepository.GetAllOrderStatuses(),
                            HasError = false,
                            ErrorMessage = string.Empty
                        };
                    }
                }
                throw new ArgumentException($"Передано некорректное значение userId = {request.UserId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"При выполнении команды по фильтрованному поиску заказов, возникла ошибка - {ex.Message}");
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
