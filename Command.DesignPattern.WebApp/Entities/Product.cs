using System.ComponentModel.DataAnnotations.Schema;

namespace Command.DesignPattern.WebApp.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Column(TypeName ="decimal(18,2)")]
        public decimal Price { get; set; }

        public short Stock { get; set; }
    }
}
