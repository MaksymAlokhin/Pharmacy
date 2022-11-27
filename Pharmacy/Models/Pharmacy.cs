using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PharmacyApp.Models
{
    public class Pharmacy
    {
        public int Id { get; set; }
        [DisplayName("Назва аптеки")]
        [Required]
        public string Name { get; set; }
        [DisplayName("Місто")]
        public string City { get; set; }
        [DisplayName("Менеджер")]
        public Manager Manager { get; set; }
        public ICollection<PharmacyMedicine> PharmacyMedicine { get; set;}
    }
}
