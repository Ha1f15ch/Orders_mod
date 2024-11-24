using DtoModelsProj;
using MediatR;
using Models;
using Repositories.InterfaceRepositories;
using SiteEngine.CommandsAndHandlers.Commands.OrderCommands;

namespace SiteEngine.CommandsAndHandlers.Handlers.OrderCommandHandlers
{
    public class GetOrderByOrderIdCommandHandler : IRequestHandler<GetOrderByOrderIdCommand, OrderDataInformation_Dto>
    {
        private readonly IUserAccauntRepository userAccauntRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IOrderPriorityRepository orderPriorityRepository;
        private readonly IOrderStatusRepository orderStatusRepository;
        private readonly IUserProfileRepository userProfileRepository;
        private readonly ICustomerUserProfileRepository customerUserProfileRepository;
        private readonly IEmployerUserProfileRepository employerUserProfileRepository;

        public GetOrderByOrderIdCommandHandler(IUserAccauntRepository userAccauntRepository,
                                               IOrderRepository orderRepository,
                                               IOrderPriorityRepository orderPriorityRepository,
                                               IOrderStatusRepository orderStatusRepository,
                                               IUserProfileRepository userProfileRepository,
                                               ICustomerUserProfileRepository customerUserProfileRepository,
                                               IEmployerUserProfileRepository employerUserProfileRepository)
        {
            this.userAccauntRepository = userAccauntRepository;
            this.orderRepository = orderRepository;
            this.orderPriorityRepository = orderPriorityRepository;
            this.orderStatusRepository = orderStatusRepository;
            this.userProfileRepository = userProfileRepository;
            this.customerUserProfileRepository = customerUserProfileRepository;
            this.employerUserProfileRepository = employerUserProfileRepository;

            Console.WriteLine("Обработчик GetOrderByOrderIdCommandHandler создан");
        }

        public async Task<OrderDataInformation_Dto> Handle(GetOrderByOrderIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var order = await orderRepository.GetOrderById(request.OrderId);
                var orderPriority = await orderPriorityRepository.GetorderPriorityById(order.OrderPriorityId);
                var orderStatus = await orderStatusRepository.GetOrderStatusById(order.OrderStatusId);
                var user = await userAccauntRepository.GetUserAccauntByUserId(request.Userid);
                
                if(user != null)
                {
                    var userProfile = await userProfileRepository.GetUserProfileByUserId(user.Id);
                    var userProfileCustomer = await userProfileRepository.GetUserProfileByUserId(order.UserIdCreated);

                    if (userProfileCustomer.UserId == userProfile.UserId)
                    {
                        var customerProfile = await customerUserProfileRepository.GetCustomerUserProfileByUserId(user.Id);

                        var employerProfile = await employerUserProfileRepository.GetEmployerUserprofileByUserId(order.UserIdAssigner);
                        var userProfileEmployer = await userProfileRepository.GetUserProfileByUserId(order.UserIdAssigner);

                        return new OrderDataInformation_Dto
                        {
                            Order = order,
                            OrderPriority = orderPriority,
                            OrderStatus = orderStatus,
                            CustomerProfile = customerProfile,
                            EmployerProfile = employerProfile,
                            Customer_UserProfile = userProfileCustomer,
                            Employer_UserProfile = userProfileEmployer,
                            HasError = false,
                        };
                    }
                }

                throw new NullReferenceException($"При поиске данных для формируемой модели в обработчике - GetOrderByOrderIdCommandHandler, возникли ошибки. Переданы некорректные данные.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"При выполнинии команды GetOrderByOrderIdCommand в обработчике GetOrderByOrderIdCommandHandler возникла ошибка - {ex.Message}");
                return new OrderDataInformation_Dto
                {
                    Order = null,
                    OrderPriority = null,
                    OrderStatus = null,
                    CustomerProfile = null,
                    EmployerProfile = null,
                    Customer_UserProfile = null,
                    Employer_UserProfile = null,
                    HasError = true,
                };
            }
        }
    }
}
