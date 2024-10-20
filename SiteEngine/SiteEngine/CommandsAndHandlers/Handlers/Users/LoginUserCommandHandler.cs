using MediatR;
using Repositories.InterfaceRepositories;
using SiteEngine.CommandsAndHandlers.Commands.Users;

namespace SiteEngine.CommandsAndHandlers.Handlers.Users
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, bool>
    {
        private readonly IUserAccauntRepository userRepository;

        public LoginUserCommandHandler(IUserAccauntRepository userRepository)
        {
            Console.WriteLine("LoginUserCommandHandler создан");
            this.userRepository = userRepository;
        }

        public async Task<bool> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await userRepository.
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка в обработчике коменд Mediatr при обработке запроса входа в систему, {ex.Message}");
                return false;
            }
        }
    }
}
