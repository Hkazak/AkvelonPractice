using Contracts.DTOs;
using Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using Persistence.Context;
using Persistence.Models;


namespace TaskManager.Controllers;

[ApiController, Route("[Controller]")]
public class UsersController : ControllerBase
{
    private readonly TaskManagerContext _context;

    public UsersController(TaskManagerContext context)
    {
        _context = context;
    }

    [HttpPost]
    public ActionResult CreateUser([FromBody] UserDTO dto)
    {
        var user = new User()
        {
            UserName = dto.UserName,
            UserSurename = dto.UserSureName,
            UserPassword = dto.UserPassword
        };
        _context.Users.Add(user);
        _context.SaveChanges();
        var response = new UserResponses()
        {
            UserId = user.UserId,
            UserName = user.UserName,
            UserSurename = user.UserSurename,
            UserPassword = user.UserPassword
        };
        return Ok(response);
    }
}