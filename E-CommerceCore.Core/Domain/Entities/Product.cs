using E_CommerceCore.Core.Domain.Entities.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceCore.Core.Domain.Entities
{
    /// <summary>
    /// Entidad Producto
    /// </summary>
    [Table("Product", Schema = "dbo")]
    public class Product : IEntity
    {
        public int Id { get; set; }
        public int IdCategory { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Stock { get; set; }

        /// <summary>
        /// Categoria Asociada al producto
        /// </summary>
        [ForeignKey("IdCategory")]
        public virtual Category Category { get; set; }
    }
}