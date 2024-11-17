using MediatR;

namespace SiteEngine.CommandsAndHandlers.Commands.CustomerCommands
{
    public class CreateCustomerUserProfileCommand : IRequest<bool>
    {
        public int Userid { get; set; }
    }
}
