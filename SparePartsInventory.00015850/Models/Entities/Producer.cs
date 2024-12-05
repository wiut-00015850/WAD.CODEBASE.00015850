using System.ComponentModel.DataAnnotations;

namespace SparePartsInventory._00015850.Models.Entities
{
    public class Producer
    {
        public int Id { get; set; }

        [MinLength(2), MaxLength(50)]
        public string Name { get; set; }

        [MinLength(2), MaxLength(50)]
        public string Country { get; set; }
        public DateTime FoundedDate { get; set; }
        public virtual ICollection<SparePart> SpareParts { get; set; }

    }
}
