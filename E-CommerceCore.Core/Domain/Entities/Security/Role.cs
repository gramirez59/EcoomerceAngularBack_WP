using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace E_CommerceCore.Core.Domain.Entities.Security
{
    /// <summary>
    /// Entidad Rol
    /// </summary>
    [Table("Role", Schema = "dbo")]
    public class Role : IdentityRole
    {
    }
}