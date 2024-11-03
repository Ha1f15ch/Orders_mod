using MediatR;
using Repositories.InterfaceRepositories;
using SiteEngine.CommandsAndHandlers.Commands.UserProfile;

namespace SiteEngine.CommandsAndHandlers.Handlers.UserProfile
{
    public class UserProfileUpdateCommandHandler : IRequestHandler<UserProfileCommand, bool>
    {
        private readonly IUserProfileRepository userProfileRepository;

        public UserProfileUpdateCommandHandler(IUserProfileRepository userProfileRepository)
        {
            this.userProfileRepository = userProfileRepository;
        }

        public async Task<bool> Handle(UserProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var findedUserProfile = await userProfileRepository.GetUserProfileByUserId(request.UserId);

                if (findedUserProfile != null)
                {
                    var resultUpdateUserProfile = await userProfileRepository.UpdateUserProfileByUserProfileId(findedUserProfile.Id, request.FirstName, request.MiddleName, request.LastName, request.Birthday);

                    if(resultUpdateUserProfile)
                    {
                        return true;
                    }
                }

                throw new NullReferenceException($"При поиске профиля пользователя по userId = {request.UserId} запись профиля пользователя найти не удлось {nameof(findedUserProfile)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Возникла ошибка при обработке команды для обновления профиля пользователя {ex.Message}");
                return false;
            }
        }
    }
}
