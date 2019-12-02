using E_CommerceCore.Core.Domain.Entities.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceCore.Core.Domain.Entities
{
    /// <summary>
    /// Entidad Categoria
    /// </summary>
    [Table("Category", Schema = "dbo")]
    public class Category : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Listado de productos
        /// </summary>
        public virtual ICollection<Product> Productos { get; set; }
    }
}