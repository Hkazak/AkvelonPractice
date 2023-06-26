namespace Contracts.Responses;

public class UserResponses
{
    public Guid UserId { get; init; }
    public string UserName { get; set; }
    public string UserSurename { get; set; }
    public string UserPassword { get; set; }
    public Guid ProjectId { get; init; }
}