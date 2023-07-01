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
    
    [HttpGet]
    public ActionResult<IEnumerable<UserResponses>> GetUsers()
    {
        var users = _context.Users.ToList();
        var response = users.Select(user => new UserResponses
        {
            UserId = user.UserId,
            UserName = user.UserName,
            UserSurename = user.UserSurename,
            UserPassword = user.UserPassword
        }).ToList();
        return Ok(response);
    }

    [HttpGet("{id}")]
    public ActionResult<UserResponses> GetUser(int id)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserId == id);
        if (user == null)
        {
            return NotFound();
        }

        var response = new UserResponses
        {
            UserId = user.UserId,
            UserName = user.UserName,
            UserSurename = user.UserSurename,
            UserPassword = user.UserPassword
        };

        return Ok(response);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateUser(int id, [FromBody] UserDTO dto)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserId == id);
        if (user == null)
        {
            return NotFound();
        }

        user.UserName = dto.UserName;
        user.UserSurename = dto.UserSureName;
        user.UserPassword = dto.UserPassword;

        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteUser(int id)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserId == id);
        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        _context.SaveChanges();

        return NoContent();
    }

}