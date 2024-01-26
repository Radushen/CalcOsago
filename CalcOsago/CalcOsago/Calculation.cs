using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Documents;

namespace CalcOsago
{
    public class Calculation // Класс основной модели данных, содержит константы-перечисления
    {
        // Переменные класса
        private WorkWith_DB data_base; // Объект для работы с базой данных

        private static Dictionary<string, double> formula_result = new Dictionary<string, double>(); // Словарь содержит формулу расчета, все коэффициенты перемножаются

        public string fio_strah { get; set; } = ""; // ФИО страхователя

        public decimal tarif_bazovi { get; set; } = 0; // Базовый тариф, по умолчанию для самарской обл. = 4118

        public int ind_ur_fiz_lico { get; set; } = 0; // Юр-физ. лицо, по умолчанию индекс 0 = физлицо

        public bool is_foregin { get; set; } = false; // Иностранец - по умолчанию - нет

        public bool is_transit { get; set; } = false; // Транспорт следует транзитом, или к месту регистрации, по умолчанию - нет

        public bool is_driver_limit { get; set; } = false; // Ограничение по водителям, по умолчанию = false

        public bool is_taxi { get; set; } = false; // Транспорт используется как такси, по умолчанию - нет

        public bool is_pricep { get; set; } = false; // Страхуется с прицепом, по умолчанию - нет

        public int power_ts { get; set; } = 0; // Мощность транспортного средства, по умолчанию = 0

        public int max_mass_ts { get; set; } = 0; // Максимальная разрешенная масса ТС

        public bool is_regul_perevoz { get; set; } = false; // Используется в регулярных перевозках, по умолчанию - нет

        public int count_pasangers { get; set; } = 0; // Количество пассажирских мест

        public int ind_categori_ts { get; set; } = 1; // Индекс выбранной категории ТС

        public bool is_warning { get; set; } = false; // "Особый" случай, по умолчанию false

        public int ind_srok_strah { get; set; } = 11; // Индекс строки срока использования ТС, по умолчанию = 11

        public int ind_srok_use { get; set; } = 8; // Индекс строки периода использования ТС, по умолчанию = 8

        public double koef_kbm { get; set; } = 1.0; // Коэффициент КБМ

        // Старый коэффициент КБМ
        public int ind_kbm { get; set; } = 5; // Индекс строки коэффициента бонус-малус

        // Индекс выбранного региона
        public int ind_region { get; set; } = 1; // Индекс выбранного региона

        public int ind_sub_region { get; set; } // Индекс под-региона

        public List<DB_Driver> Drivers { get; set; } = new List<DB_Driver>(); // Список допущенных водителей



        // Служебные процедуры и функции
        public decimal CalcStrahPrem() // Возвращает результирующую страховую премию
        {
            decimal result = 1; // Результирующая страховая премия
                                // Определяем формулу, по которой будет расчет. В определении участвуют показатели: физ. лицо, иностранец, транзит и категория ТС.
                                // Формула записывается в словарь, т.к. все коэффициенты перемножаются
            Dictionary<string, double> main_formula = new Dictionary<string, double>();
            if (!is_foregin && !is_transit) // Если это РФ и не следует к месту регистрации ТС
            {
                if (ind_ur_fiz_lico == 0) // Вычисляем для российского физ.лица
                {

                    if (ind_categori_ts == 1) // Если категория "B", "BE" в т.ч. такси
                    {
                        main_formula.Add("Tb:", 1);  // Базовый тариф
                        main_formula.Add("Kt:", 1);  // Коэффициент территориальный
                        main_formula.Add("Kbm:", 1); // Коэффициент бонус-малус по водителю
                        main_formula.Add("Kvs:", 1); // Коэффициент возраста стажа водителя
                        main_formula.Add("Ko:", 1);  // С ограничением по водителям
                        main_formula.Add("Km:", 1);  // Коэффициент мощности ТС
                        main_formula.Add("Ks:", 1);  // Период использования ТС
                        main_formula.Add("Kn:", 1);  // "особый случай"
                    }
                    else // Для остальных категорий
                    {
                        main_formula.Add("Tb:", 1);  // Базовый тариф
                        main_formula.Add("Kt:", 1);  // Коэффициент территориальный
                        main_formula.Add("Kbm:", 1); // Коэффициент бонус-малус по водителю
                        main_formula.Add("Kvs:", 1); // Коэффициент возраста стажа водителя
                        main_formula.Add("Ko:", 1);  // С ограничением по водителям
                        main_formula.Add("Ks:", 1);  // Период использования ТС
                        main_formula.Add("Kn:", 1);  // "особый случай"
                        main_formula.Add("Kpr:", 1); // Используется с прицепом
                    }
                }
                else // Вычисляем для российского юр.лица
                {
                    if (ind_categori_ts == 1) // Если категория "B", "BE" в т.ч. такси
                    {
                        main_formula.Add("Tb:", 1);  // Базовый тариф
                        main_formula.Add("Kt:", 1);  // Коэффициент территориальный
                        main_formula.Add("Kbm:", 1); // Коэффициент бонус-малус по водителю
                        main_formula.Add("Ko:", 1);  // С ограничением по водителям
                        main_formula.Add("Km:", 1);  // Коэффициент мощности ТС
                        main_formula.Add("Ks:", 1);  // Период использования ТС
                        main_formula.Add("Kn:", 1);  // "особый случай"
                        main_formula.Add("Kpr:", 1); // Используется с прицепом
                    }
                    else // Для остальных категорий
                    {
                        main_formula.Add("Tb:", 1);  // Базовый тариф
                        main_formula.Add("Kt:", 1);  // Коэффициент территориальный
                        main_formula.Add("Kbm:", 1); // Коэффициент бонус-малус по водителю
                        main_formula.Add("Ko:", 1);  // С ограничением по водителям
                        main_formula.Add("Ks:", 1);  // Период использования ТС
                        main_formula.Add("Kn:", 1);  // "особый случай"
                        main_formula.Add("Kpr:", 1); // Используется с прицепом
                    }
                }
            }
            else if (!is_foregin && is_transit) // Если это РФ и следует к месту регистрации тс
            {
                if (ind_ur_fiz_lico == 0) // Вычисляем для физ.лица
                {
                    if (ind_categori_ts == 1) // Если категория "B", "BE" в т.ч. такси
                    {
                        main_formula.Add("Tb:", 1);  // Базовый тариф
                        main_formula.Add("Kbm:", 1); // Коэффициент бонус-малус по водителю
                        main_formula.Add("Kvs:", 1); // Коэффициент возраста стажа водителя
                        main_formula.Add("Ko:", 1);  // С ограничением по водителям
                        main_formula.Add("Km:", 1);  // Коэффициент мощности ТС
                        main_formula.Add("Kp:", 1);  // Срок страхования
                    }

                    else // Для всех остальных
                    {
                        main_formula.Add("Tb:", 1);  // Базовый тариф
                        main_formula.Add("Kbm:", 1); // Коэффициент бонус-малус по водителю
                        main_formula.Add("Kvs:", 1); // Коэффициент возраста стажа водителя
                        main_formula.Add("Ko:", 1);  // С ограничением по водителям
                        main_formula.Add("Kp:", 1);  // Срок страхования
                        main_formula.Add("Kpr:", 1); // Используется с прицепом
                    }
                }
                else // Вычисляем для юр.рица
                {
                    if (ind_categori_ts == 1) // Если категория "B", "BE" в т.ч. такси
                    {
                        main_formula.Add("Tb:", 1);  // Базовый тариф
                        main_formula.Add("Kbm:", 1); // Коэффициент бонус-малус по водителю
                        main_formula.Add("Ko:", 1);  // С ограничением по водителям
                        main_formula.Add("Km:", 1);  // Коэффициент мощности ТС
                        main_formula.Add("Kp:", 1);  // Срок страхования
                        main_formula.Add("Kpr:", 1); // Используется с прицепом
                    }

                    else // Для всех остальных категорий
                    {
                        main_formula.Add("Tb:", 1);  // Базовый тариф
                        main_formula.Add("Kbm:", 1); // Коэффициент бонус-малус по водителю
                        main_formula.Add("Ko:", 1);  // С ограничением по водителям
                        main_formula.Add("Kp:", 1);  // Срок страхования
                        main_formula.Add("Kpr:", 1); // Используется с прицепом
                    }
                }
            }
            else if (is_foregin) // Если это иностранец
            {
                if (ind_ur_fiz_lico == 0) // Вычисляем для физ.лица
                {
                    if (ind_categori_ts == 1) // Если категория "B", "BE" в т.ч. такси
                    {
                        main_formula.Add("Tb:", 1);  // Базовый тариф
                        main_formula.Add("Kt:", 1);  // Коэффициент территориальный
                        main_formula.Add("Kbm:", 1); // Коэффициент бонус-малус по водителю
                        main_formula.Add("Kvs:", 1); // Коэффициент возраста стажа водителя
                        main_formula.Add("Ko:", 1);  // С ограничением по водителям
                        main_formula.Add("Km:", 1);  // Коэффициент мощности ТС
                        main_formula.Add("Kp:", 1);  // Срок страхования
                        main_formula.Add("Kn:", 1);  // "особый случай"
                    }
                    else // Для всех остальных категорий
                    {
                        main_formula.Add("Tb:", 1);  // Базовый тариф
                        main_formula.Add("Kt:", 1);  // Коэффициент территориальный
                        main_formula.Add("Kbm:", 1); // Коэффициент бонус-малус по водителю
                        main_formula.Add("Kvs:", 1); // Коэффициент возраста стажа водителя
                        main_formula.Add("Ko:", 1);  // С ограничением по водителям
                        main_formula.Add("Kp:", 1);  // Срок страхования
                        main_formula.Add("Kn:", 1);  // "Особый случай"
                        main_formula.Add("Kpr:", 1); // Используется с прицепом
                    }
                }
                else // Вычисляем для юр.рица
                {
                    if (ind_categori_ts == 1) // Если категория "B", "BE" в т.ч. такси
                    {
                        main_formula.Add("Tb:", 1);  // Базовый тариф
                        main_formula.Add("Kt:", 1);  // Коэффициент территориальный
                        main_formula.Add("Kbm:", 1); // Коэффициент бонус-малус по водителю
                        main_formula.Add("Ko:", 1);  // С ограничением по водителям
                        main_formula.Add("Km:", 1);  // Коэффициент мощности ТС
                        main_formula.Add("Kp:", 1);  // Срок страхования
                        main_formula.Add("Kn:", 1);  // "особый случай"
                        main_formula.Add("Kpr:", 1); // Используется с прицепом
                    }
                    else // Для остальных категорий
                    {
                        main_formula.Add("Tb:", 1);  // Базовый тариф
                        main_formula.Add("Kt:", 1);  // Коэффициент территориальный
                        main_formula.Add("Kbm:", 1); // Коэффициент бонус-малус по водителю
                        main_formula.Add("Ko:", 1);  // С ограничением по водителям
                        main_formula.Add("Kp:", 1);  // Срок страхования
                        main_formula.Add("Kn:", 1);  // "особый случай"
                        main_formula.Add("Kpr:", 1); // Используется с прицепом
                    }
                }
            }

            formula_result = FillFormulaResult(main_formula);
            foreach (var tmp_val in formula_result) // Перебираем элементы словаря и перемножаем их
            {
                result = result * (decimal)tmp_val.Value;
            }

            /* 
            вычисляем максимальную страховую премию.
            Премия не может превышать значения (3*tmp_tb*tmp_kt) при обычных условиях
            и премия не может превышать значения (5*tmp_tb*tmp_kt) при особых условиях
            */

            double tmp_tb = GetKoeff_Tb();
            double tmp_kt = GetKoeff_Kt();
            if (!is_warning)
            {
                if (result > (decimal)(3 * tmp_tb * tmp_kt))
                {
                    result = (decimal)(3 * tmp_tb * tmp_kt);
                }
            }
            else
            {
                if (result > (decimal)(5 * tmp_tb * tmp_kt))
                {
                    result = (decimal)(5 * tmp_tb * tmp_kt);
                }
            }
            return result;

        }



        public Dictionary<string, double> FillFormulaResult(Dictionary<string, double> formula) // Функция обрабатывает исходную формулу, и заполняет необходимые коэффициенты
        {
            Dictionary<string, double> result_dict = new Dictionary<string, double>(); // Результат вычислений
            foreach (var tmp_record in formula) // Проверяем каждый элемент словаря и вычисляем необходимые коэффициенты
            {
                switch (tmp_record.Key)
                {
                    case "Tb:": // Базовый тариф
                        {
                            double koeff_Tb = GetKoeff_Tb();
                            result_dict.Add(tmp_record.Key, koeff_Tb); // Добавляем в словарь коэффициент и его значение
                            break;
                        }

                    case "Kt:": // Коэффициент территориальный
                        {
                            double koeff_Kt = GetKoeff_Kt();
                            result_dict.Add(tmp_record.Key, koeff_Kt); // Добавляем в словарь коэффициент и его значение
                            break;
                        }

                    case "Kbm:": // Коэффициент бонус-малус
                        {
                            double koeff_Kbm = GetKoeff_Kbm();
                            result_dict.Add(tmp_record.Key, koeff_Kbm); // Добавляем в словарь коэффициент и его значение
                            break;
                        }

                    case "Kvs:": // Коэффициент возраста стажа водителя
                        {
                            double koeff_Kvs = GetKoeff_Kvs();
                            result_dict.Add(tmp_record.Key, koeff_Kvs); // Добавляем в словарь коэффициент и его значение
                            break;
                        }

                    case "Ko:": // С ограничением по водителям
                        {
                            double koeff_Ko = GetKoeff_Ko();
                            result_dict.Add(tmp_record.Key, koeff_Ko); // Добавляем в словарь коэффициент и его значение
                            break;
                        }

                    case "Km:": // Коэффициент мощности ТС
                        {
                            double koeff_Km = GetKoeff_Km(power_ts);
                            result_dict.Add(tmp_record.Key, koeff_Km); // Добавляем в словарь коэффициент и его значение
                            break;
                        }

                    case "Ks:": // Период использования ТС
                        {
                            double koef_Ks = GetKoeff_Ks();
                            result_dict.Add(tmp_record.Key, koef_Ks); // Добавляем в словарь коэффициент и его значение
                            break;
                        }

                    case "Kn:": // "особый случай"
                        {
                            double koeff_Kn = GetKoeff_Kn();
                            result_dict.Add(tmp_record.Key, koeff_Kn); // Добавляем в словарь коэффициент и его значение
                            break;
                        }

                    case "Kp:": // Срок страхования
                        {
                            double koeff_Kp = GetKoeff_Kp();
                            result_dict.Add(tmp_record.Key, koeff_Kp); // Добавляем в словарь коэффициент и его значение
                            break;
                        }

                    case "Kpr:": // Использование с прицепом
                        {
                            double koeff_Kpr = GetKoeff_Kpr();
                            result_dict.Add(tmp_record.Key, koeff_Kpr); // Добавляем в словарь коэффициент и его значение
                            break;
                        }
                }
            }
            return result_dict;
        }



        private double GetKoeff_Kpr() // Коэффициент использования прицепа
        {
            double result = 0;
            if (is_pricep)
            {
                if ((ind_categori_ts == 0 || ind_categori_ts == 1) & ind_ur_fiz_lico == 1) // Если категория ТС А или B и принадлежащие юр.лицам
                {
                    result = 1.16;
                }
                else if (ind_categori_ts == 2 & max_mass_ts <= 16) // Если категория ТС С и менне 16 тонн разрешенная масса
                {
                    result = 1.4;
                }
                else if (ind_categori_ts == 2 & max_mass_ts > 16) // Если категория ТС С и более 16 тонн разрешенная масса
                {
                    result = 1.25;
                }
                else if (ind_categori_ts == 6)  // Если тракторы, самоходки
                {
                    result = 1.24;
                }
                else // К другим категориям и типам
                {
                    result = 1;
                }
            }
            else
            {
                result = 1;
            }
            return result;
        }



        private double GetKoeff_Kp() // Функция возвращает коэффициент сроков страхования
        {
            double result = 0;
            if (!is_foregin) // Если это россиянин
            {
                result = 0.2;
            }
            else // Если это иностранец
            {
                result = data_base._get_koeff_Kp(ind_srok_strah); // Выбираем данные из БД, в параметры функции передается индекс записи сроков страхования
            }
            return result;
        }

        private double GetKoeff_Kn() // Функция возвращает коэффициент особого случая
        {
            double result = 0; // Если это "особый случай"

            if (is_warning)
            {
                result = 1.5;
            }
            else
            {
                result = 1;
            }
            return result;
        }

        private double GetKoeff_Ks() // Функция возвращает коэффициент периода использования ТС
        {
            double result = 0;
            result = data_base._get_koef_Ks(ind_srok_use);
            return result;
        }

        static double GetKoeff_Km(int power_ts) // Функция возвращает коэффициент мощности ТС с помощью абстрактного класса
        {
            KM a = new KM(power_ts);
            double result = a.CheckCondition(power_ts);
            return result;
        }

        private double GetKoeff_Ko() // Функция возвращает коэффициент ограничения по водителям
        {
            double result = 0;

            if (is_driver_limit)
            {
                result = 1;
            }
            else
            {
                result = 1.87;
            }
            if (ind_ur_fiz_lico == 1) // Если юр.лицо
            {
                result = 1.8;
            }

            return result;
        }

        private double GetKoeff_Kvs() // Функция возвращает коэффициент возраста-стажа
        {
            double result = 0; // Если это не иностранец
            if (is_driver_limit)
            {
                if (!is_foregin)
                {
                    result = get_max_kvs(Drivers); // Получаем максимальный коэффициент по водителям
                }
                else // Для иностранца
                {
                    if (ind_ur_fiz_lico == 0) // Для физлица
                    {
                        result = 1.7;
                    }
                    else // Для юрлица
                    {
                        result = 1;
                    }
                }
            }
            else
            {
                result = 1;
            }
            return result;
        }

        private double get_max_kvs(List<DB_Driver> drivers) // Функция возвращает максимальный коэффциент по списку водителей
        {
            double result = 0;

            if (drivers.Count > 0)
            {
                fill_drivers_kvs(drivers); // Заполняем список водителей коэффициентами
                double tmp_result = 0; // Получаем максимальный коэффициент	
                foreach (DB_Driver tmp_driver in drivers)
                {
                    if (tmp_result < tmp_driver.Koeff)
                    {
                        tmp_result = tmp_driver.Koeff;
                    }
                }
                result = tmp_result;
            }
            else
            {
                result = 0;
            }
            return result;
        }

        private void fill_drivers_kvs(List<DB_Driver> drivers) // Процедура заполняет данные значениями полученных коэффициентов
        {
            foreach (DB_Driver tmp_driver in drivers) // Перебираем записи списка водителей и устанавливаем соответствующий коэффициент
            {

                int age_driver = get_diff_years(tmp_driver.AgeDriver); // Вычисляем возраст водителя
                int stage_driver = get_diff_years(tmp_driver.StageDriver); // Вычисляем стаж водителя
                if (stage_driver < age_driver)
                {
                    tmp_driver.Koeff = get_kvs(age_driver, stage_driver);
                }
                else
                {
                    tmp_driver.Koeff = 0;
                }

            }
        }

        static double get_kvs(int driver_age, int driver_stage) // Функция возвращает коэффициент возраста стажа c помощью абстрактного класса
        {
            KVS a = new KVS(driver_age, driver_stage);
            double result = a.CheckCondition(driver_age, driver_stage);
            return result;
        }

        private double GetKoeff_Kbm() // Функция возвращает коэффициет бонус-малус
        {
            double result = 0.0;
            if (is_driver_limit) // Если c ограничениями
            {
                result = get_max_kbm_driver();
            }
            else
            {
                result = koef_kbm;
            }
            return result;
        }

        private double get_max_kbm_driver() // Функция возвращает максимальный коэффициент по водителям
        {
            double result = 0.0; // Определяем максимальный коэффициент
            foreach (DB_Driver tmp_driver in Drivers)
            {
                if (tmp_driver.Koeff_kbm.koeff > result)
                {
                    result = tmp_driver.Koeff_kbm.koeff;
                }
            }
            return result;
        }

        private double GetKoeff_Kt() // Функция возвращает значение территориального коэфициента
        {
            double result = 0;
            if (!is_foregin) // Если это не иностранец
            {
                result = data_base._get_koef_Kt(ind_sub_region, ind_categori_ts);
            }
            else // Для иностранца
            {
                result = 1.7;
            }
            return result;
        }

        private double GetKoeff_Tb() // Функция возвращает значение коэффициента базового тарифа
        {
            double result = 0;
            result = data_base._get_base_tarif(ind_categori_ts, ind_sub_region, ind_ur_fiz_lico, is_taxi, max_mass_ts, count_pasangers, is_regul_perevoz);
            return result;
        }

        public string GetResultFormula() // Возвращает формулу расчета в текстовом представлении
        {
            StringBuilder result = new StringBuilder(); // Переменная хранит результаты расчета
            int tmp_ind = 1; // Счетчик количества записей
            foreach (var tmp_val in formula_result) // Перебираем коллекцию и вводим ее в строку
            {
                result = result.Append(tmp_val.Key + "\t" + tmp_val.Value.ToString());
                if (tmp_ind < formula_result.Count)
                {
                    result = result.Append("\n");
                    tmp_ind++;
                }
            }
            return result.ToString();
        }

        public void ResetSetters() // Процедура сброса установленных показателей
        {
            formula_result.Clear(); // Очищаем словарь основной формулы
            // Устанавливаем параметры по умолчанию
            fio_strah = "";
            koef_kbm = 1.0;
            tarif_bazovi = 0;
            ind_ur_fiz_lico = 0;
            is_foregin = false;
            is_driver_limit = false;
            ind_kbm = 5;
            is_taxi = false;
            is_pricep = false;
            ind_categori_ts = 1;
            is_transit = false;
            power_ts = 0;
            max_mass_ts = 0;
            is_warning = false;
            is_regul_perevoz = false;
            count_pasangers = 0;
            ind_srok_strah = 11;
            ind_srok_use = 8;
            ind_region = 1;
            ind_sub_region = 0;
            Drivers.Clear();
        }

        private int get_diff_years(DateTime tmp_date) // Функция возвращает количество лет от даты
        {
            int result = 0;

            if (tmp_date < DateTime.Now)
            {
                DateTime zeroTime = new DateTime(1, 1, 1);
                TimeSpan span = DateTime.Today - tmp_date;
                result = (zeroTime + span).Year - 1;
            }

            return result;
        }

        //Работа с базой данных
        public ObservableCollection<DB_Person> GetUrFizLicoList() // Функция получает перечисления юр.физ.лицо из базы данных
        {
            ObservableCollection<DB_Person> result = new ObservableCollection<DB_Person>();
            result = data_base._getUrFizLico();
            return result;
        }

        public ObservableCollection<DB_CategoriaTS> GetCategoriaTs() // Функция получает список категорий ТС
        {
            ObservableCollection<DB_CategoriaTS> result = new ObservableCollection<DB_CategoriaTS>();
            result = data_base._getCateoriaTs();
            return result;
        }

        public ObservableCollection<DB_PeriodInsurance> GetPeriodStrahTs() // Функция возвращает коллекцию периодов страхования
        {
            ObservableCollection<DB_PeriodInsurance> result = new ObservableCollection<DB_PeriodInsurance>();
            result = data_base._getPeriodStrah();
            return result;
        }

        public ObservableCollection<DB_PeriodUse> GetPeriodUseTs() // Функция возвращает коллекцию периодов использования
        {
            ObservableCollection<DB_PeriodUse> result = new ObservableCollection<DB_PeriodUse>();
            result = data_base._getPeriodUse();
            return result;
        }

        public ObservableCollection<DB_Region> GetRegions() // Функция возвращает коллекцию регионов
        {
            ObservableCollection<DB_Region> result = new ObservableCollection<DB_Region>();
            result = data_base._getRegions();
            return result;
        }

        public ObservableCollection<DB_SubRegion> GetSubRegions(int subRegions) // Функция возвращает под-регионы
        {
            ObservableCollection<DB_SubRegion> result = new ObservableCollection<DB_SubRegion>();
            result = data_base._getSubRegions(subRegions);
            return result;
        }

        public ObservableCollection<DB_KbmCl> GetKbms() // Функция возвращает список КБМ
        {
            ObservableCollection<DB_KbmCl> result = new ObservableCollection<DB_KbmCl>();
            result = data_base._getKbmList();
            return result;
        }

        public DataTable GetDataTableBaseTarif() // Метод получает данные из таблицы базовых тарифов
        {
            DataTable result = new DataTable();
            result = data_base._get_dt_base_tarif();
            return result;
        }

        public Calculation()
        {
            data_base = new WorkWith_DB(); // Инициализируем объект для работы с базой данных
        }
    }
}
