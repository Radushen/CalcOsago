using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalcOsago
{
   
    public class DB_KbmCl // Класс для хранения коэффициента бонус-малус
    {
        public int id { get; set; } // Идентификатор записи

        public string text_value { get; set; } // Текстовое предствление

        public double koeff { get; set; } = 0; // Значение коэффициента КБМ
    }
}
