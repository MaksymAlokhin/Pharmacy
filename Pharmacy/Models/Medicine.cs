using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyApp.Models
{
    public class Medicine
    {
        public int Id { get; set; }
        [StringLength(50, ErrorMessage = "Довжина не повинна перевищуати 50 символів.")]
        [DisplayName("Найменування")]
        [Required]
        public string Name { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        [DisplayName("Ціна")]
        public decimal Price { get; set; }
        [DisplayName("Зображеня")]
        public string BoxArt { get; set; }
        public ICollection<PharmacyMedicine> PharmacyMedicine { get; set; }
    }
}
