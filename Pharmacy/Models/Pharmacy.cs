using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PharmacyApp.Models
{
    public class Pharmacy
    {
        public int Id { get; set; }
        [StringLength(50, ErrorMessage = "Довжина не повинна перевищуати 50 символів.")]
        [DisplayName("Назва аптеки")]
        [Required]
        public string Name { get; set; }
        [StringLength(50, ErrorMessage = "Довжина не повинна перевищуати 50 символів.")]
        [DisplayName("Місто")]
        public string City { get; set; }
        [DisplayName("Менеджер")]
        public Manager Manager { get; set; }
        public ICollection<PharmacyMedicine> PharmacyMedicine { get; set;}
    }
}
