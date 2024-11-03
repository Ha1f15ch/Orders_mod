using DtoModelsProj;
using MediatR;
using Repositories.InterfaceRepositories;
using SiteEngine.CommandsAndHandlers.Commands.UserProfile;
using SiteEngine.Models.UserProfiles;

namespace SiteEngine.CommandsAndHandlers.Handlers.UserProfile
{
    public class GetUserProfileHandler : IRequestHandler<GetUserProfileCommand, UserProfileModel>
    {
        private readonly IUserProfileRepository userProfileRepository;

        public GetUserProfileHandler(IUserProfileRepository userProfileRepository)
        {
            this.userProfileRepository = userProfileRepository;
            Console.WriteLine("GetUserProfileHandler создан");
        }

        public async Task<UserProfileModel> Handle(GetUserProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userProfileFromEF = await userProfileRepository.GetUserProfileByUserId(request.UserId);

                if (userProfileFromEF == null)
                {
                    Console.WriteLine($"Найти запись профиля пользоватекля неоплучлиось {nameof(userProfileFromEF)}");
                    throw new NullReferenceException("Не удалось найти запись по заданному id");
                }
                else
                {
                    var resultModel = new UserProfileModel()
                    {
                        FirstName = userProfileFromEF.FirstName,
                        MiddleName = userProfileFromEF.MiddleName,
                        LastName = userProfileFromEF.LastName,
                        Birthday = userProfileFromEF.Birthday,
                    };

                    return resultModel;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Возникла ошиька при обработке команды для получения профиля пользователя - {ex.Message}");
                return new UserProfileModel();
            }
        }
    }
}
