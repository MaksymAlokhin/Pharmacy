using System.ComponentModel;

namespace PharmacyApp.Models
{
    public class Manager
    {
        public int Id { get; set; }
        [DisplayName("Ім'я")]
        public string FirstName { get; set; }
        [DisplayName("Прізвище")]
        public string LastName { get; set; }
        [DisplayName("По батькові")]
        public string Patronymic { get; set; }
        public string FullName => $"{FirstName} {LastName} {Patronymic}";
        public string Photo { get; set; }
        public int? PharmacyId { get; set; }
        [DisplayName("Місце роботи")]
        public Pharmacy Pharmacy { get; set; }
    }
}
