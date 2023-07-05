using Contracts.DTOs;
using Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using Persistence.Context;
using Persistence.Models;
using TaskManager.Services;


namespace TaskManager.Controllers;

[ApiController, Route("[Controller]")]
public class UsersController : ControllerBase
{
    private readonly TaskManagerContext _context;
    private readonly UserServices _userServices;

    public UsersController(TaskManagerContext context, UserServices userServices)
    {
        _context = context;
        _userServices = userServices;
    }

    [HttpPost]
    public async Task<ActionResult<UserResponses>> CreateUser([FromBody] UserDTO dto)
    {
        var response = await _userServices.CreateUserAsync(dto);
        return Ok(response);
    }
    
    [HttpDelete]
    public async Task<ActionResult> DeleteUser([FromRoute] Guid id)
    {
        await _userServices.DeleteUserAsync(id);
        return NoContent();
    }
    
}