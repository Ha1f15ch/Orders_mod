using MediatR;
using Repositories.InterfaceRepositories;
using SiteEngine.CommandsAndHandlers.Commands.CommonCommand;

namespace SiteEngine.CommandsAndHandlers.Handlers.CommonCommandHandlers
{
    public class GetUserProfilesByUserIdCommandHandler : IRequestHandler<GetUserProfilesByUserIdCommand, IDictionary<string, object?>>
    {
        private readonly ICommonProfileData commonProfileData;
        private readonly IUserAccauntRepository userAccauntRepository;
        
        public GetUserProfilesByUserIdCommandHandler(ICommonProfileData commonProfileData)
        {
            this.commonProfileData = commonProfileData;
            Console.WriteLine("GetUserProfilesByUserIdCommandHandler создан");
        }

        public async Task<IDictionary<string, object?>> Handle(GetUserProfilesByUserIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if(request.UserId > 0)
                {
                    if(await userAccauntRepository.ReturnHasUser(request.UserId))
                    {
                        return await commonProfileData.ReturnUserProfilesByUserId(request.UserId);
                    } 
                }

                Console.WriteLine($"Возникла ошибка при выполнении команды - GetUserProfilesByUserIdCommandHandler. Ошибка - значение userId не соответсвует условиям {nameof(request.UserId)}. Либо не найдена запись User по данному id = {request.UserId}");
                return new Dictionary<string, object?>()
                {
                    {"UserProfile", null },
                    {"CustomerProfile", null },
                    {"EmployerProfile", null }
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Возникла ошибка при выполнении команды - GetUserProfilesByUserIdCommandHandler. Ошибка - {ex.Message}");
                return new Dictionary<string, object?>()
                {
                    {"UserProfile", null },
                    {"CustomerProfile", null },
                    {"EmployerProfile", null }
                };
            }
        }
    }
}
