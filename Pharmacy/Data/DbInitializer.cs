using System.Linq;

namespace PharmacyApp.Data
{
    public static class DbInitializer
    {
        public static void Initialize(PharmacyContext context)
        {
            if (!context.Managers.Any())
            {
                context.AddRange(SeedManagers.data);
            }
            if (!context.Medicines.Any())
            {
                context.AddRange(SeedMedicines.data);
            }
            if (!context.Pharmacies.Any())
            {
                context.AddRange(SeedPharmacies.data);
            }
            if (!context.PharmacyMedicine.Any())
            {
                context.AddRange(SeedPharmacyMedicine.data);
            }
            context.SaveChanges();
        }
    }
}