using PharmacyApp.Models;
using System.Collections.Generic;

namespace PharmacyApp.Data
{
    public static class SeedMedicines
    {
        #region Create medicines

        public static Medicine med01 = new Medicine
        {
            Name = "Ехінацея Імуно капсули №20",
            Price = 79.00M,
            BoxArt = "med01.jpg"
        };

        public static Medicine med02 = new Medicine
        {
            Name = "Епігалін Брест капсули №30",
            Price = 462.10M,
            BoxArt = "med02.jpg"
        };

        public static Medicine med03 = new Medicine
        {
            Name = "Декрістол D3 4000 МЕ таблетки №30",
            Price = 285.70M,
            BoxArt = "med03.jpg"
        };

        public static Medicine med04 = new Medicine
        {
            Name = "Батончик Гена та Ген з альбуміном 50 г",
            Price = 18.10M,
            BoxArt = "med04.jpg"
        };

        public static Medicine med05 = new Medicine
        {
            Name = "Смарт Омега для дітей капсули №30",
            Price = 192.87M,
            BoxArt = "med05.jpg"
        };

        public static Medicine med06 = new Medicine
        {
            Name = "Анаферон дитячий таблетки №20",
            Price = 107.33M,
            BoxArt = "med06.jpg"
        };

        public static Medicine med07 = new Medicine
        {
            Name = "Омега-3 капсули №60",
            Price = 206.40M,
            BoxArt = "med07.jpg"
        };

        public static Medicine med08 = new Medicine
        {
            Name = "Вітамін С Ацерола капсули №10",
            Price = 42.60M,
            BoxArt = "med08.jpg"
        };

        public static Medicine med09 = new Medicine
        {
            Name = "Аміксин ІС 0,125 г таблетки №3",
            Price = 198.00M,
            BoxArt = "med09.jpg"
        };

        public static Medicine med10 = new Medicine
        {
            Name = "Протефлазід краплі 50 мл",
            Price = 727.50M,
            BoxArt = "med10.jpg"
        };

        public static Medicine med11 = new Medicine
        {
            Name = "Према саше №10",
            Price = 238.10M,
            BoxArt = "med11.jpg"
        };

        public static Medicine med12 = new Medicine
        {
            Name = "L-Тироксин 100 Берлін-Хемі 100 мкг таблетки №50",
            Price = 108.32M,
            BoxArt = "med12.jpg"
        };

        public static Medicine med13 = new Medicine
        {
            Name = "Кислота бурштинова 0,25 г таблетки №80",
            Price = 28.60M,
            BoxArt = "med13.jpg"
        };

        public static Medicine med14 = new Medicine
        {
            Name = "Доппельгерц Актив Омега-3 чисті судини капсули №30",
            Price = 269.40M,
            BoxArt = "med14.jpg"
        };

        public static Medicine med15 = new Medicine
        {
            Name = "Флувір Kids порошок саше №10",
            Price = 280.80M,
            BoxArt = "med15.jpg"
        };

        public static Medicine med16 = new Medicine
        {
            Name = "Фемара 2,5 мг таблетки №30",
            Price = 1333.70M,
            BoxArt = "med16.jpg"
        };

        public static Medicine med17 = new Medicine
        {
            Name = "Золадекс 3,6 мг капсули та шприц-аплікатор №1",
            Price = 2531.90M,
            BoxArt = "med17.jpg"
        };

        public static Medicine med18 = new Medicine
        {
            Name = "Джерело ПІ фітоконцентрат 30 мл",
            Price = 49.00M,
            BoxArt = "med18.jpg"
        };

        public static Medicine med19 = new Medicine
        {
            Name = "Летрозол-Тева 2,5 мг таблетки №30",
            Price = 553.80M,
            BoxArt = "med19.jpg"
        };

        public static Medicine med20 = new Medicine
        {
            Name = "Алфавіт у Сезон Застуд таблетки №60",
            Price = 227.46M,
            BoxArt = "med20.jpg"
        };
        #endregion
        public static List<Medicine> data;
        static SeedMedicines()
        {
            data = new List<Medicine>();
            data.Add(med01);
            data.Add(med02);
            data.Add(med03);
            data.Add(med04);
            data.Add(med05);
            data.Add(med06);
            data.Add(med07);
            data.Add(med08);
            data.Add(med09);
            data.Add(med10);
            data.Add(med11);
            data.Add(med12);
            data.Add(med13);
            data.Add(med14);
            data.Add(med15);
            data.Add(med16);
            data.Add(med17);
            data.Add(med18);
            data.Add(med19);
            data.Add(med20);
        }
    }
}
