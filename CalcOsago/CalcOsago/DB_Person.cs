using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalcOsago
{
    public class DB_Person
    {
        public int id_cl { get; set; } = 0; // Идентификатор перечисления

        public string value_cl { get; set; } = "физ. лицо"; // Значение перечисления

        public override string ToString()// Переопределенная функция возврата в строковом представлении
        {
            return value_cl;
        }
    }
}
