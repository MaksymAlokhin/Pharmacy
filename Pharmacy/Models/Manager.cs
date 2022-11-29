using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PharmacyApp.Models
{
    public class Manager
    {
        public int Id { get; set; }
        [StringLength(50, ErrorMessage = "Довжина не повинна перевищуати 50 символів.")]
        [DisplayName("Ім'я")]
        public string FirstName { get; set; }
        [StringLength(50, ErrorMessage = "Довжина не повинна перевищуати 50 символів.")]
        [DisplayName("Прізвище")]
        [Required]
        public string LastName { get; set; }
        [StringLength(50, ErrorMessage = "Довжина не повинна перевищуати 50 символів.")]
        [DisplayName("По батькові")]
        public string Patronymic { get; set; }
        public string FullName => $"{LastName} {FirstName} {Patronymic}";
        public string ShortName => $"{LastName} {FirstName.Substring(0, 1)}.{Patronymic.Substring(0, 1)}.";
        [DisplayName("Фото")]
        public string Photo { get; set; }
        public int? PharmacyId { get; set; }
        [DisplayName("Місце роботи")]
        public Pharmacy Pharmacy { get; set; }
    }
}
