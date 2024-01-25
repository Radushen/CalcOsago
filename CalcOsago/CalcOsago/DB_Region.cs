using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalcOsago
{
    public class DB_Region
    {
        public int id { get; set; } // Идентификатор записи

        public string text_value { get; set; } // Текстовое представление записи

        public int id_categori_teritory { get; set; } // Индекс категории территории
    }
}
