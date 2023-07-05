using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Models;

public class User 
{ 
    [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid UserId { get; init; }
     public string UserName { get; set; }
     public string UserSurename { get; set; }
     public string UserPassword { get; set; }
     public ICollection<Project> Projects { get; init; } = new List<Project>();
    protected bool Equals(User other)
    {
        return UserId == other.UserId && UserName == other.UserName && UserSurename == other.UserSurename;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((User)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(UserId, UserName, UserSurename);
    }
}