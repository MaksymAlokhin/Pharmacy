using System.ComponentModel;

namespace PharmacyApp.Models
{
    public class PharmacyMedicine
    {
        public int Id { get; set; }
        [DisplayName("К-ть")]
        public int Quantity { get; set; }
        public int PharmacyId { get; set; }
        public int MedicineId { get; set; }
        [DisplayName("Аптека")]
        public Pharmacy Pharmacy { get; set; }
        [DisplayName("Найменування")]
        public Medicine Medicine { get; set; }
    }
}
