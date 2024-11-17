using MediatR;

namespace SiteEngine.CommandsAndHandlers.Commands.EmployerCommands
{
    public class CreateEmployerUserProfileCommand : IRequest<bool>
    {
        public int UserId { get; set; }
    }
}
