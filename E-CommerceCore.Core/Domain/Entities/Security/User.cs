using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace E_CommerceCore.Core.Domain.Entities.Security
{
    /// <summary>
    /// Entidad Usuario
    /// </summary>
    [Table("User", Schema = "dbo")]
    public class User : IdentityUser
    {
    }
}