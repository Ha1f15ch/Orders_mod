using MediatR;
using Repositories.InterfaceRepositories;
using SiteEngine.CommandsAndHandlers.Commands.Users;


namespace SiteEngine.CommandsAndHandlers.Handlers.Users
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, bool>
    {
        private readonly IUserAccauntRepository userRepository;
        private readonly IUserRoleRepository userRoleRepository;

        public RegisterUserCommandHandler(IUserAccauntRepository userRepository, IUserRoleRepository userRoleRepository)
        {
            Console.WriteLine("RegisterUserCommandHandler создан");
            this.userRepository = userRepository;
            this.userRoleRepository = userRoleRepository;
        }

        public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if(await userRepository.CreateUserAccaunt(request.UserName, request.UserEmail, request.UserPassword, request.UserPhoneNumber))
                {
                    var user = await userRepository.GetUserAccauntByUserName(request.UserName);

                    await userRoleRepository.SetDefaultUserRole(user.Id);

                    return true;
                }
                else
                {
                    Console.WriteLine("ошибка при создании аккаунта user и привязке роли user в обработкичке RegisterUserCommandHandler");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка в обработчике комaнд Mediatr при обработке запроса регистрации, {ex.Message}");
                return false;
            }
        }
    }
}
