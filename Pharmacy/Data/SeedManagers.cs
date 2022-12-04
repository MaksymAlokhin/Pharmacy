using PharmacyApp.Models;
using System.Collections.Generic;

namespace PharmacyApp.Data
{
    public static class SeedManagers
    {
        #region Create managers
        public static Manager mgr01 = new Manager
        {
            LastName = "Мазурик",
            FirstName = "Шаміль",
            Patronymic = "Денисович",
            Photo = "male_001.jpg"
        };

        public static Manager mgr02 = new Manager
        {
            LastName = "Шишацький",
            FirstName = "Юрій",
            Patronymic = "Фролович",
            Photo = "male_002.jpg"
        };

        public static Manager mgr03 = new Manager
        {
            LastName = "Полтавець",
            FirstName = "Златодан",
            Patronymic = "Азарович",
            Photo = "male_003.jpg"
        };

        public static Manager mgr04 = new Manager
        {
            LastName = "Андрієвич",
            FirstName = "Княжослав",
            Patronymic = "Іванович",
            Photo = "male_004.jpg"
        };

        public static Manager mgr05 = new Manager
        {
            LastName = "Бережнюк",
            FirstName = "Іларіон",
            Patronymic = "Любомирович",
            Photo = "male_005.jpg"
        };

        public static Manager mgr06 = new Manager
        {
            LastName = "Шкрібляк",
            FirstName = "Цецілія",
            Patronymic = "Северинівна",
            Photo = "female_001.jpg"
        };

        public static Manager mgr07 = new Manager
        {
            LastName = "Голота",
            FirstName = "Феодосія",
            Patronymic = "Іванівна",
            Photo = "female_002.jpg"
        };

        public static Manager mgr08 = new Manager
        {
            LastName = "Черкасенко",
            FirstName = "Зінаїда",
            Patronymic = "Захарівна",
            Photo = "female_003.jpg"
        };

        public static Manager mgr09 = new Manager
        {
            LastName = "Дриженко",
            FirstName = "Неля",
            Patronymic = "Герасимівна",
            Photo = "female_004.jpg"
        };

        public static Manager mgr10 = new Manager
        {
            LastName = "Малишенко",
            FirstName = "Маргарита",
            Patronymic = "Давидівна",
            Photo = "female_005.jpg"
        };
        #endregion
        public static List<Manager> data;
        static SeedManagers()
        {
            data = new List<Manager>();
            data.Add(mgr01);
            data.Add(mgr02);
            data.Add(mgr03);
            data.Add(mgr04);
            data.Add(mgr05);
            data.Add(mgr06);
            data.Add(mgr07);
            data.Add(mgr08);
            data.Add(mgr09);
            data.Add(mgr10);
        }
    }
}
