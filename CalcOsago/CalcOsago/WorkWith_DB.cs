using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace CalcOsago
{
    class WorkWith_DB // Класс предназначен для работы с базой данных

    {
        private SQLiteConnection connect; // Соединение для работы с файлом - источником данных

        public ObservableCollection<DB_Person> _getUrFizLico() // Метод получения данных юр.физ. лиц
        {

            ObservableCollection<DB_Person> tmp_collection_data = new ObservableCollection<DB_Person>(); // Коллекция для временного хранения данных
            if (connect != null & connect.State == System.Data.ConnectionState.Open) // Получаем данные из таблицы
            {
                string sql_command = "SELECT * FROM UrFizLico"; // Создаем команду чтения данных
                SQLiteCommand cmd = new SQLiteCommand(sql_command, connect); // Считываем построчно данные и записываем их в коллекцию
                try
                {
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DB_Person tmp_data = new DB_Person();
                        tmp_data.id_cl = Int32.Parse((reader["id_urfiz_lico"].ToString()));
                        tmp_data.value_cl = reader["text_urfiz_lico"].ToString();
                        tmp_collection_data.Add(tmp_data);
                    }
                }
                catch
                {
                }
            }
            return tmp_collection_data;
        }

        public ObservableCollection<DB_CategoriaTS> _getCateoriaTs() // метод получает список категорий ТС
        {

            ObservableCollection<DB_CategoriaTS> tmp_collection_data = new ObservableCollection<DB_CategoriaTS>(); // Коллекция для временного хранения данных
            if (connect != null & connect.State == System.Data.ConnectionState.Open) // Получаем данные из таблицы
            {

                string sql_command = "SELECT * FROM KategoriaTS"; // Создаем команду чтения данных
                SQLiteCommand cmd = new SQLiteCommand(sql_command, connect); // Считываем построчно данные и записываем их в коллекцию
                try
                {
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DB_CategoriaTS tmp_data = new DB_CategoriaTS();
                        tmp_data.id_categoria_ts = Int32.Parse((reader["id_categoria_ts"].ToString()));
                        tmp_data.kategoria_ts = reader["kategoria_ts"].ToString();
                        tmp_collection_data.Add(tmp_data);
                    }
                }
                catch
                {
                }
            }
            return tmp_collection_data;
        }


        public ObservableCollection<DB_PeriodInsurance> _getPeriodStrah() // Метод получает коллекцию периодов страхования
        {

            ObservableCollection<DB_PeriodInsurance> tmp_collection_data = new ObservableCollection<DB_PeriodInsurance>(); // Коллекция для временного хранения данных  
            if (connect != null & connect.State == System.Data.ConnectionState.Open) // Получаем данные из таблицы
            {
                string sql_command = "SELECT * FROM PeriodStrahTS"; // Создаем команду чтения данных
                SQLiteCommand cmd = new SQLiteCommand(sql_command, connect); // Считываем построчно данные и записываем их в коллекцию
                try
                {
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DB_PeriodInsurance tmp_data = new DB_PeriodInsurance();
                        tmp_data.id = Int32.Parse((reader["id"].ToString()));
                        tmp_data.text_value = reader["text_period_strah_ts"].ToString();
                        tmp_collection_data.Add(tmp_data);
                    }
                }
                catch
                {
                }
            }
            return tmp_collection_data;
        }


        public ObservableCollection<DB_PeriodUse> _getPeriodUse() // Метод получает коллекцию периодов использования
        {
            ObservableCollection<DB_PeriodUse> tmp_collection_data = new ObservableCollection<DB_PeriodUse>(); // Коллекция для временного хранения данных
            if (connect != null & connect.State == System.Data.ConnectionState.Open) // Получаем данные из таблицы
            {
                string sql_command = "SELECT * FROM PeriodUseTS"; // Создаем команду чтения данных
                SQLiteCommand cmd = new SQLiteCommand(sql_command, connect); // Считываем построчно данные и записываем их в коллекцию
                try
                {
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DB_PeriodUse tmp_data = new DB_PeriodUse();
                        tmp_data.id = Int32.Parse((reader["id"].ToString()));
                        tmp_data.text_value = reader["text_period_use_ts"].ToString();
                        tmp_collection_data.Add(tmp_data);
                    }
                }
                catch
                {
                }
            }
            return tmp_collection_data;
        }


        public ObservableCollection<DB_Region> _getRegions() // Метод получает коллекцию регионов
        {
            ObservableCollection<DB_Region> tmp_collection_data = new ObservableCollection<DB_Region>(); // Коллекция для временного хранения данных
            if (connect != null & connect.State == System.Data.ConnectionState.Open) // Получаем данные из таблицы
            {
                string sql_command = "SELECT * FROM Regions"; // Создаем команду чтения данных
                SQLiteCommand cmd = new SQLiteCommand(sql_command, connect); // Считываем построчно данные и записываем их в коллекцию
                try
                {
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DB_Region tmp_data = new DB_Region();
                        tmp_data.id = Int32.Parse(reader["id_region"].ToString());
                        tmp_data.text_value = reader["region_name"].ToString();
                        tmp_collection_data.Add(tmp_data);
                    }
                }
                catch
                {
                }
            }
            return tmp_collection_data;
        }

        public ObservableCollection<DB_SubRegion> _getSubRegions(int ind_main_region)
        {
            ObservableCollection<DB_SubRegion> tmp_collection_data = new ObservableCollection<DB_SubRegion>(); //коллекция для временного хранения данных
            if (connect != null & connect.State == System.Data.ConnectionState.Open) // получаем данные из таблицы
            {

                string sql_command = "SELECT sub_regions.id_sub_region," + "sub_regions.region," + "sub_regions.sub_region_name " + "FROM SubRegions sub_regions " + "WHERE sub_regions.region =" + ind_main_region.ToString(); // Создаем команду чтения данных
                SQLiteCommand cmd = new SQLiteCommand(sql_command, connect); // Считываем построчно данные и записываем их в коллекцию
                try
                {
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DB_SubRegion tmp_data = new DB_SubRegion();
                        tmp_data.id = Int32.Parse((reader["id_sub_region"].ToString()));
                        tmp_data.text_value = reader["sub_region_name"].ToString();
                        tmp_collection_data.Add(tmp_data);
                    }
                }
                catch
                {
                }
            }
            return tmp_collection_data;
        }


        public ObservableCollection<DB_KbmCl> _getKbmList() // Функция возвращает список коэффициентов КБМ
        {
            ObservableCollection<DB_KbmCl> tmp_collection_data = new ObservableCollection<DB_KbmCl>(); // Коллекция для временного хранения данных
            if (connect != null & connect.State == System.Data.ConnectionState.Open) // Получаем данные из таблицы
            {
                string sql_command = "SELECT * FROM KBM"; // Создаем команду чтения данных
                SQLiteCommand cmd = new SQLiteCommand(sql_command, connect); // Считываем построчно данные и записываем их в коллекцию
                try
                {
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DB_KbmCl tmp_data = new DB_KbmCl();
                        tmp_data.id = Int32.Parse((reader["id"].ToString()));
                        tmp_data.text_value = reader["text_value"].ToString();
                        string tmp_value = reader["koef"].ToString().Replace(".", ",");
                        tmp_data.koeff = Convert.ToDouble(tmp_value);
                        tmp_collection_data.Add(tmp_data);
                    }
                }
                catch
                {
                }
            }
            return tmp_collection_data;
        }


        public DataTable _get_dt_base_tarif() // Функция возвращает таблицу данных базовых тарифов
        {
            DataTable result = new DataTable();
            if (connect != null & connect.State == System.Data.ConnectionState.Open)
            {
                string sql_command = "SELECT * FROM BaseTarif"; // Создаем команду чтения данных из таблицы базовых тарифов
                SQLiteCommand cmd = new SQLiteCommand(sql_command, connect); // Пытаемся прочитать данные
                try
                {
                    SQLiteDataReader reader = cmd.ExecuteReader(); // Выгружаем выбранные данные в таблицу
                    result.Load(reader);
                }
                catch
                {
                }
            }
            return result;
        }


        // Определение значений коэффициентов
        public double _get_koeff_Kp(int index) // Функция возвращает значение коэффициента, в зависимости от индекса записи
        {
            double result = 0; // Получаем данные из таблицы
            if (connect != null & connect.State == System.Data.ConnectionState.Open)
            {

                string sql_command = "SELECT period_strah.id," + "period_strah.koef " + "FROM PeriodStrahTS period_strah " + "WHERE period_strah.id =" + index.ToString(); // Создаем команду чтения данных
                SQLiteCommand cmd = new SQLiteCommand(sql_command, connect); // Считываем построчно данные и записываем их в коллекцию
                try
                {
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result = (double)reader["koef"];
                    }
                }
                catch
                {
                }
            }
            return result;
        }


        public double _get_koef_Ks(int index) // Функция возвращает значение коэффициента периода использования
        {
            double result = 0;
            if (connect != null & connect.State == System.Data.ConnectionState.Open) // Получаем данные из таблицы
            {

                string sql_command = "SELECT period_use.id," + "period_use.koef " + "FROM PeriodUseTS period_use " + "WHERE period_use.id =" + index.ToString(); // Создаем команду чтения данных
                SQLiteCommand cmd = new SQLiteCommand(sql_command, connect); // Считываем построчно данные и записываем их в коллекцию
                try
                {
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result = (double)reader["koef"];
                    }
                }
                catch
                {
                }
            }
            return result;
        }


        public double _get_koef_Kbm(int index) // Функция возвращает коэффициент
        {
            double result = 0;
            if (connect != null & connect.State == System.Data.ConnectionState.Open) // Получаем данные из таблицы
            {
                // Создаем команду чтения данных
                string sql_command = "SELECT kbm.id," + "kbm.koef " + "FROM KBM kbm " + "WHERE kbm.id =" + index.ToString();
                SQLiteCommand cmd = new SQLiteCommand(sql_command, connect); // Считываем построчно данные и записываем их в коллекцию
                try
                {
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result = (double)reader["koef"];
                    }
                }
                catch
                {
                }
            }
            return result;
        }


        public double _get_koef_Kt(int index_sub_region, int ind_kategori = 1) // Функция возвращает коэффициент территории использования
        {
            double result = 0;
            if (connect != null & connect.State == System.Data.ConnectionState.Open) // Получаем данные из таблицы
            {
                string sql_command = "SELECT subregion.id_sub_region," + "subregion.koef_all, " + "subregion.koef_traktor " + "FROM SubRegions subregion " + "WHERE subregion.id_sub_region =" + index_sub_region.ToString(); // Создаем команду чтения данных
                SQLiteCommand cmd = new SQLiteCommand(sql_command, connect); // Считываем построчно данные и записываем их в коллекцию
                try
                {
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (ind_kategori == 6)
                        {
                            result = (double)reader["koef_traktor"];
                        }
                        else
                        {
                            result = (double)reader["koef_all"];
                        }
                    }
                }
                catch
                {
                }
            }
            return result;
        }


        public double _get_base_tarif(int ind_categor_ts, int ind_sub_region, int ind_urfiz_lico, bool use_taxi, int weght, int seat, bool regular_perevoz) // Функция возвращает значение базового тарифа
        {
            double result = 0;
            if (connect != null & connect.State == System.Data.ConnectionState.Open) // Получаем данные из таблицы
            {

                string sql_command = "SELECT * " + "FROM BaseTarif base_tarif " + "WHERE base_tarif.id_categir_ts =" + ind_categor_ts.ToString() + " AND base_tarif.id_sub_region =" + ind_sub_region.ToString(); // Создаем команду чтения данных
                SQLiteCommand cmd = new SQLiteCommand(sql_command, connect); // Считываем построчно данные и записываем их в коллекцию
                try
                {
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (ind_categor_ts == 0)  // Для категорий
                        {
                            result = (double)reader["koef"];
                            break;
                        }
                        else if (ind_categor_ts == 1)
                        {
                            if (ind_urfiz_lico == 0 & !use_taxi)
                            {
                                result = (double)reader["fiz_lico"];
                            }
                            else if (ind_urfiz_lico == 1 & !use_taxi)
                            {
                                result = (double)reader["ur_lico"];
                            }
                            else if (use_taxi)
                            {
                                result = (double)reader["taxi"];
                            }
                        }
                        else if (ind_categor_ts == 2)
                        {
                            if (weght <= 16)
                            {
                                result = (double)reader["min_weght"];
                            }
                            else
                            {
                                result = (double)reader["max_weght"];
                            }
                        }
                        else if (ind_categor_ts == 3)
                        {
                            if (seat <= 16 & !regular_perevoz)
                            {
                                result = (double)reader["min_seat"];
                            }
                            else if (seat > 16 & !regular_perevoz)
                            {
                                result = (double)reader["max_seat"];
                            }
                            else if (regular_perevoz)
                            {
                                result = (double)reader["regular_perevoz"];
                            }
                        }
                        else if (ind_categor_ts == 4)
                        {
                            result = (double)reader["koef"];
                        }
                        else if (ind_categor_ts == 5)
                        {
                            result = (double)reader["koef"];
                        }
                        else if (ind_categor_ts == 6)
                        {
                            result = (double)reader["koef"];
                        }
                    }
                }
                catch
                {
                }
            }
            return result;
        }


        public WorkWith_DB() // В конструкторе производим подключение к базе данных
        {
            // Подключение к БД
            try
            {
                string path_to_file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "main_db.db"); // Получаем относительный путь к файлу с базой данных
                connect = new SQLiteConnection("Data Source=" + path_to_file + "; Version=3;");
                connect.Open(); // Открываем базу данных
            }
            catch
            {
            }
        }
    }
}
