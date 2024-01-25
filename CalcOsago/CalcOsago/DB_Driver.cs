using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalcOsago
{
   
    public class DB_Driver // Класс предназначен для работы с водителем
    {
        public string NameDriver { get; set; } = ""; // ФИО водителя

        public DateTime AgeDriver { get; set; } = new DateTime(1980, 1, 1); // Возраст водителя

        public DateTime StageDriver { get; set; } = new DateTime(2000, 1, 1); // Водительский стаж

        public double Koeff { get; set; } // Коэффициент возраста стажа

        public DB_KbmCl Koeff_kbm { get; set; } = new DB_KbmCl(); // Коэффициент бонсу-малус
    }
}
