using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SparePartsInventory._00015850.Models.Entities
{
    public class SparePart
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [MaxLength(5_000)]
        public string Description { get; set; }

        [Range(1, 1_000_000)]
        [Precision(10, 2)]
        public decimal Price { get; set; }

        //foreign key
        public int ProducerId { get; set; }
        public Producer Producer { get; set; }

    }
}
