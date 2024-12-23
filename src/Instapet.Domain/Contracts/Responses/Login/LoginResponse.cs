namespace Instapet.Domain.Contracts.Responses.Login;

public class LoginResponse
{
    
    public bool IsAuthenticated { get; set; } = false;

    public Guid Id{ get; set; }
    public string Email { get; set; }
}