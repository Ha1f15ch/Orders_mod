using MediatR;
using Repositories.InterfaceRepositories;
using SiteEngine.CommandsAndHandlers.Commands.UserProfile;

namespace SiteEngine.CommandsAndHandlers.Handlers.UserProfile
{
    public class UserProfileCommandHandler : IRequestHandler<UserProfileCommand, bool>
    {
        private readonly IUserAccauntRepository userRepository;
        private readonly IUserProfileRepository userProfileRepository;

        public UserProfileCommandHandler(IUserAccauntRepository userRepository, IUserProfileRepository userProfileRepository)
        {
            this.userRepository = userRepository;
            this.userProfileRepository = userProfileRepository;
            Console.WriteLine("UserProfileCommandHandler - создан");
        }

        public async Task<bool> Handle(UserProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var resultByCreateduserprofile = await userProfileRepository.CreateUserProfile(request.UserId, request.FirstName, request.MiddleName, request.LastName, request.Birthday);

                if(resultByCreateduserprofile)
                {
                    return true;
                }
                else
                {
                    throw new Exception("ОШибка при создании профиля пользователя");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Возникшая ошибка - {ex.Message}");
                return false;
            }
        }
    }
}
