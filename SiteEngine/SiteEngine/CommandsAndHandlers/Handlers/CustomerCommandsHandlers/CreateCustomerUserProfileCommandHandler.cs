using MediatR;
using Repositories.InterfaceRepositories;
using SiteEngine.CommandsAndHandlers.Commands.CustomerCommands;

namespace SiteEngine.CommandsAndHandlers.Handlers.CustomerCommandsHandlers
{
    public class CreateCustomerUserProfileCommandHandler : IRequestHandler<CreateCustomerUserProfileCommand, bool>
    {
        private readonly ICustomerUserProfileRepository customerUserProfileRepository;

        public CreateCustomerUserProfileCommandHandler(ICustomerUserProfileRepository customerUserProfileRepository)
        {
            this.customerUserProfileRepository = customerUserProfileRepository;
            Console.WriteLine($"Обработчик CreateCustomerUserProfileCommandHandler создан");
        }

        public async Task<bool> Handle(CreateCustomerUserProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if(request.Userid > 0)
                {
                    if(await customerUserProfileRepository.CreateCustomerUserProfile(request.Userid)) return true;
                }

                return false;
            }
            catch ( Exception ex )
            {
                Console.WriteLine($"Возникла ошибка при выполнении команды по созданию профиля заказчика - {ex.Message}");
                return false;
            }
        }
    }
}
