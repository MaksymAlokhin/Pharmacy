using PharmacyApp.Models;
using System.Collections.Generic;
using static PharmacyApp.Data.SeedManagers;

namespace PharmacyApp.Data
{
    public static class SeedPharmacies
    {
        #region Create pharmacies

        public static Pharmacy phar01 = new Pharmacy
        {
            Name = "Аптека АНЦ",
            City = "Київ",
            Manager = mgr01
        };

        public static Pharmacy phar02 = new Pharmacy
        {
            Name = "Бажаємо здоров'я",
            City = "Черкаси",
            Manager = mgr02
        };

        public static Pharmacy phar03 = new Pharmacy
        {
            Name = "Аптека 911",
            City = "Львів",
            Manager = mgr03
        };

        public static Pharmacy phar04 = new Pharmacy
        {
            Name = "Мережа аптек «Подорожник»",
            City = "Іванофранківськ",
            Manager = mgr04
        };

        public static Pharmacy phar05 = new Pharmacy
        {
            Name = "Об’єднання аптечних мереж Аснова",
            City = "Дніпропетровськ",
            Manager = mgr05
        };

        public static Pharmacy phar06 = new Pharmacy
        {
            Name = "Аптека Доброго Дня",
            City = "Вінниця",
            Manager = mgr06
        };

        public static Pharmacy phar07 = new Pharmacy
        {
            Name = "Аптека Мед-сервіс",
            City = "Житомир",
            Manager = mgr07
        };

        public static Pharmacy phar08 = new Pharmacy
        {
            Name = "Мережа аптек D.S.",
            City = "Хмельницький",
            Manager = mgr08
        };

        public static Pharmacy phar09 = new Pharmacy
        {
            Name = "Farmacia",
            City = "Тернопіль",
            Manager = mgr09
        };

        public static Pharmacy phar10 = new Pharmacy
        {
            Name = "Здорова родина",
            City = "Луцьк",
            Manager = mgr10
        };
        #endregion
        public static List<Pharmacy> data;
        static SeedPharmacies()
        {
            data = new List<Pharmacy>();
            data.Add(phar01);
            data.Add(phar02);
            data.Add(phar03);
            data.Add(phar04);
            data.Add(phar05);
            data.Add(phar06);
            data.Add(phar07);
            data.Add(phar08);
            data.Add(phar09);
            data.Add(phar10);
        }
    }
}
