using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PharmacyApp.Models
{
    public class Medicine
    {
        public int Id { get; set; }
        [DisplayName("Найменування")]
        [Required]
        public string Name { get; set; }
        [DisplayName("Ціна")]
        public decimal Price { get; set; }
        [DisplayName("Зображеня")]
        public string BoxArt { get; set; }
        public ICollection<PharmacyMedicine> PharmacyMedicine { get; set; }
    }
}
