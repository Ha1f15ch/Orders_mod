using MediatR;
using Repositories.InterfaceRepositories;
using SiteEngine.CommandsAndHandlers.Commands.EmployerCommands;

namespace SiteEngine.CommandsAndHandlers.Handlers.EmployerCommandsHandlers
{
    public class CreateEmployerUserProfileCommandHandler : IRequestHandler<CreateEmployerUserProfileCommand, bool>
    {
        private readonly IEmployerUserProfileRepository employerUserProfileRepository;

        public CreateEmployerUserProfileCommandHandler(IEmployerUserProfileRepository employerUserProfileRepository)
        {
            this.employerUserProfileRepository = employerUserProfileRepository;
            Console.WriteLine("Обработкик CreateEmployerUserProfileCommandHandler создан");
        }

        public async Task<bool> Handle(CreateEmployerUserProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.UserId > 0)
                {
                    if(await employerUserProfileRepository.CreateEmployerUserProfile(request.UserId)) return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Возникла ошибка при выполнении команды по созданию профиля исполнителя - {ex.Message}");
                return false;
            }
        }
    }
}
