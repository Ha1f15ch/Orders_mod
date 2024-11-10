using MediatR;
using Repositories.InterfaceRepositories;
using SiteEngine.CommandsAndHandlers.Commands.UserProfile;

namespace SiteEngine.CommandsAndHandlers.Handlers.UserProfile
{
    public class HasUserProfileCommandHandler : IRequestHandler<HasUserProfileCommand, bool>
    {
        private readonly IUserProfileRepository userProfileRepository;

        public HasUserProfileCommandHandler(IUserProfileRepository userProfileRepository)
        {
            this.userProfileRepository = userProfileRepository;
            Console.WriteLine("Обработчик HasUserProfileCommandHandler создан");
        }

        public async Task<bool> Handle(HasUserProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if(request.UserId > 1)
                {
                    if(await userProfileRepository.HasUserProfileByUserId(request.UserId)) return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Возникла ошибка при определении наличия у пользователя профиля пользователя - {ex.Message}");
                return false;
            }
        }

    }
}
