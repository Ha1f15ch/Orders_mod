namespace SiteEngine.CommandsAndHandlers.Commands.Users
{
    public class LoginUserResult
    {
        public bool IsSuccess { get; set; }
        public int UserId { get; set; }
        public string ErrorMessage { get; set; }
    }
}
