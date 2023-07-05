using Contracts.DTOs;
using Contracts.Responses;
using Persistence.Context;
using Persistence.Models;

namespace TaskManager.Services;

public class UserServices
{
    private readonly TaskManagerContext _context;

    public UserServices(TaskManagerContext context)
    {
        _context = context;
    }

    public async Task<UserResponses> CreateUserAsync(UserDTO dto)
    {
        var user = new User
        {
            UserName = dto.UserName,
            UserSurename = dto.UserSureName,
            UserPassword = dto.UserPassword
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        var response = new UserResponses
        {
            UserName = user.UserName,
            UserSurename = user.UserSurename,
            UserPassword = user.UserPassword
        };
        return response;
    }

    public async Task<UserResponses> DeleteUserAsync(Guid id)
    {
        var user = _context.Users.FirstOrDefault(x => x.UserId == id);
        if (user is null)
        {
            throw new Exception($"Project with ID {id} not found");
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return null;    
    }
    
}