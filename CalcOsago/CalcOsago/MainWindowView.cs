using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CalcOsago
{
    public class ViewModelMainWindow : INotifyPropertyChanged, INotifyDataErrorInfo // Наследуется от интерфейса INotifyPropertyChanged
    {
        // Переменные класса       
        private Calculation model_data; // Основная модель данных

        public string FioStrah // ФИО страхователя
        {
            get
            {
                return model_data.fio_strah;
            }
            set
            {
                model_data.fio_strah = value;
                OnPropertyChanged("FioStrah");
            }
        }

        public ObservableCollection<DB_Driver> Drivers { get; set; } = new ObservableCollection<DB_Driver>(); // Список водителей, допущенных к управлению

        public DB_Driver SelectedDriver { get; set; } // Выбранный водитель в списке

        public ObservableCollection<DB_Person> DictUrFizLico { get; set; } // Коллекция хранит перечисления - юр.физ. лицо


        // Свойство отвечает за индекс выбранного юр.физ. лица, индекс 0 = физлицо
        public DB_Person SelectedUrFizLicoIndex // Для юр.лица есть определнные рамки
        {
            get
            {
                if (model_data.ind_ur_fiz_lico == 1)
                {
                    return DictUrFizLico[0];
                }
                else
                {
                    return DictUrFizLico[1];
                }
            }
            set
            {
                model_data.ind_ur_fiz_lico = value.id_cl;
                OnPropertyChanged("SelectedUrFizLicoIndex");
                if (value.id_cl == 1) // Устанавливаем значения по умолчанию для юр.лиц
                {
                    // Периоды страхования и использования устанавливаются 10 мес. и более
                    SelectedPeriodUse = get_period_use_by_index(8);
                    BlockPeriodUse = false;
                    OnPropertyChanged("BlockPeriodUse");
                    SelectedPeriodStrah = get_srok_strah_by_index(11);
                    OnPropertyChanged("SelectedPeriodUse");
                    // Без ограничения
                    IsDriverLimit = false;
                    OnPropertyChanged("IsDriverLimit");
                    BlockDriverLimit = false;
                    OnPropertyChanged("BlockDriverLimit");
                }
                else
                {
                    BlockPeriodUse = true;
                    OnPropertyChanged("BlockPeriodUse");
                    BlockDriverLimit = true;
                    OnPropertyChanged("BlockDriverLimit");
                }
            }
        }

        public ObservableCollection<DB_CategoriaTS> ListCategoriaTs { get; set; } // Коллекция категорий ТС

        public DB_CategoriaTS SelectedCategoriaTS // Выбранная категория ТС
        {
            get
            {
                return get_categori_ts_by_index(model_data.ind_categori_ts);
            }
            set
            {
                model_data.ind_categori_ts = value.id_categoria_ts;
                OnPropertyChanged("SelectedCategoriaTS");
            }
        }

        private DB_CategoriaTS get_categori_ts_by_index(int index) // Функция осуществляет поиск записи в коллекции категорий тс по индексу
        {
            DB_CategoriaTS tmp_result = new DB_CategoriaTS();
            foreach (DB_CategoriaTS tmp_value in ListCategoriaTs) // Перебираем все элементы массива, пока не найдем нужный
            {
                if (tmp_value.id_categoria_ts == index) // Если находим равный указанному
                {
                    tmp_result = tmp_value;
                    break;
                }
            }
            return tmp_result;
        }

        public ObservableCollection<DB_PeriodInsurance> ListPeriodStrah { get; set; } // Период страхования

        public DB_PeriodInsurance SelectedPeriodStrah // Выбранный период страхования
        {
            get
            {
                return get_srok_strah_by_index(model_data.ind_srok_strah);
            }
            set
            {
                model_data.ind_srok_strah = value.id;
                OnPropertyChanged("SelectedPeriodStrah");
            }
        }

        private DB_PeriodInsurance get_srok_strah_by_index(int index) // Функция осуществляет поиск записи в коллекции периодов страхования
        {
            DB_PeriodInsurance result = new DB_PeriodInsurance();
            foreach (DB_PeriodInsurance tmp_value in ListPeriodStrah) // Перебираем все элементы массива, пока не найдем нужный
            {
                if (tmp_value.id == index) // Если находим равный указанному
                {
                    result = tmp_value;
                    break;
                }
            }
            return result;
        }

        public ObservableCollection<DB_PeriodUse> ListPeriodUse { get; set; } // Период использования ТС

        public DB_PeriodUse SelectedPeriodUse // Выбранный период использования
        {
            get
            {
                return get_period_use_by_index(model_data.ind_srok_use);
            }
            set
            {
                model_data.ind_srok_use = value.id;
                OnPropertyChanged("SelectedPeriodUse");
            }
        }

        private DB_PeriodUse get_period_use_by_index(int index) // Функция осуществляет поиск периода использования по индексу
        {
            DB_PeriodUse result = new DB_PeriodUse();

            foreach (DB_PeriodUse tmp_value in ListPeriodUse) // Перебираем все элементы массива, пока не найдем нужный
            {
                if (tmp_value.id == index) // Если находим равный указанному
                {
                    result = tmp_value;
                    break;
                }
            }
            return result;
        }

        public bool BlockPeriodUse { get; set; } = true; // Признак блокирования списка периода использования ТС

        public ObservableCollection<DB_Region> ListRegions { get; set; } // Список регионов

        public DB_Region SelectedRegion // Выбранный регион
        {
            get { return get_selected_region(model_data.ind_region); }
            set
            {
                model_data.ind_region = value.id;
                // Устанавливаем уточнение территории, выбираем подчиненные записи
                ListSubRegions = model_data.GetSubRegions(model_data.ind_region);
                OnPropertyChanged("ListSubRegions");
                // Устанавливаем первый элемент
                SelectedSubRegion = ListSubRegions[0];
                OnPropertyChanged("SelectedSubRegion");
            }
        }

        private DB_Region get_selected_region(int index) // Выбранный регион, необходим для установки под-регионов
        {
            DB_Region result = new DB_Region();
            foreach (DB_Region tmp_value in ListRegions) // Перебираем все элементы массива, пока не найдем нужный
            {
                if (tmp_value.id == index) // Если находим равный указанному
                {
                    result = tmp_value;
                    break;
                }
            }
            return result;
        }

        public bool EnableRegions { get; set; } = true; // Признак блокирования списка регионов

        public ObservableCollection<DB_SubRegion> ListSubRegions { get; set; } = new ObservableCollection<DB_SubRegion>(); // Коллекция под-регионов



        public DB_SubRegion SelectedSubRegion // Выбранный под-регион
        {
            get
            {
                return get_subregion_by_index(model_data.ind_sub_region);
            }
            set
            {
                if (value != null)
                {
                    model_data.ind_sub_region = value.id;
                    OnPropertyChanged("SelectedSubRegion");
                }
            }
        }

        private DB_SubRegion get_subregion_by_index(int index) // Функция возвращает объект класса по индексу списка
        {
            DB_SubRegion result = new DB_SubRegion();
            // Перебираем все элементы массива, пока не найдем нужный
            foreach (DB_SubRegion tmp_value in ListSubRegions)
            {
                // Если находим равный указанному
                if (tmp_value.id == index)
                {
                    result = tmp_value;
                    break;
                }
            }

            return result;
        }

        public bool EnableSubRegions { get; set; } = true; // Признак блокирования списка под-регионов

        public ObservableCollection<DB_KbmCl> ListKbm { get; set; } // Коэффициент КБМ
        public double NewKbm
        {
            get { return model_data.koef_kbm; }
            set
            {
                model_data.koef_kbm = value;
                OnPropertyChanged("NewKbm");
            }
        }

        public double ValueKbm { get; set; } // Значение коэффициента КБМ

        public DB_KbmCl SelectedKbm // Выбранный коэффициент бонус-малуса
        {
            get
            {
                return get_kbm_by_index(model_data.ind_kbm);
            }
            set
            {
                model_data.ind_kbm = value.id;
                OnPropertyChanged("SelectedKbm");
            }
        }

        private DB_KbmCl get_kbm_by_index(int index) // Функция возвращает элемент коллекции КБМ
        {
            DB_KbmCl result = new DB_KbmCl();

            foreach (DB_KbmCl tmp_value in ListKbm) // Перебираем все элементы массива, пока не найдем нужный
            {
                if (tmp_value.id == index) // Если находим равный указанному
                {
                    result = tmp_value;
                    break;
                }
            }
            return result;
        }

        public bool BlockKbm { get; set; } = true; // Признак блокирования выбора коэффициента КБМ

        public bool IsTransit // Транспорт следует транзитом, или к месту регистрации
        {
            get
            {
                return model_data.is_transit;
            }
            set
            {
                model_data.is_transit = value;
                if (value == true) // Если это транзит, тогда устанавливаем сроки страхования
                {
                    SelectedPeriodStrah = get_srok_strah_by_index(1);
                    IsForegin = false;
                    OnPropertyChanged("IsForegin");
                }
                OnPropertyChanged("IsTransit");
                OnPropertyChanged("SelectedPeriodStrah");
            }
        }

        public bool IsForegin // Признак иностранца
        {
            get
            {
                return model_data.is_foregin;
            }
            set
            {
                model_data.is_foregin = value;
                OnPropertyChanged("IsForegin");
                if (value == true)
                {
                    IsTransit = false;
                    OnPropertyChanged("IsTransit");
                    EnableRegions = false;
                    OnPropertyChanged("EnableRegions");
                    EnableSubRegions = false;
                    OnPropertyChanged("EnableSubRegions");
                    // устанавливаем регион иностранца
                    //model_data.ind_region = 87;
                    SelectedRegion = get_selected_region(87);
                    OnPropertyChanged("SelectedRegion");
                }
                else
                {
                    EnableRegions = true;
                    OnPropertyChanged("EnableRegions");
                    EnableSubRegions = true;
                    OnPropertyChanged("EnableSubRegions");
                    // устанавливаем регион
                    SelectedRegion = get_selected_region(1);
                    OnPropertyChanged("SelectedRegion");
                }
            }
        }

        public bool IsDriverLimit // Признак ограничения по водителям
        {
            get
            {
                return model_data.is_driver_limit;
            }
            set
            {
                model_data.is_driver_limit = value;
                OnPropertyChanged("IsDriverLimit");
                if (!value) // Если снимаем флаг, тогда список водителей очищается
                {
                    model_data.Drivers.Clear();
                    Drivers.Clear();
                    OnPropertyChanged("Drivers");
                    BlockKbm = true; // Блокируем выбор коэффициента КБМ
                }
                else
                {
                    BlockKbm = false;
                }
                OnPropertyChanged("BlockKbm");
            }
        }

        public bool BlockDriverLimit { get; set; } = true; // Признак блокирования "галки" ограничения по водителям

        public bool IsTaxi // Используется в качестве такси
        {
            get
            {
                return model_data.is_taxi;
            }
            set
            {
                model_data.is_taxi = value;
                OnPropertyChanged("IsTaxi");
            }
        }

        public bool IsPricep // Может использоваться с прицепом
        {
            get
            {
                return model_data.is_pricep;
            }
            set
            {
                model_data.is_pricep = value;
                OnPropertyChanged("IsPricep");
            }
        }

        public int PowerTS // Мощность ТС в л.с.
        {
            get
            {
                return model_data.power_ts;
            }
            set
            {
                if (!(SelectedCategoriaTS.id_categoria_ts == 1 && value == 0)) // Если валидное значение, удаляем сообщения по ошибкам
                {
                    ClearErrors("PowerTS");
                }
                model_data.power_ts = value;
                OnPropertyChanged("PowerTS");
            }
        }

        public int MaxMassaTS // Максимально разрешенная масса ТС
        {
            get
            {
                return model_data.max_mass_ts;
            }
            set
            {
                if (!(SelectedCategoriaTS.id_categoria_ts == 2 && MaxMassaTS == 0))
                {
                    ClearErrors("MaxMassaTS");
                }
                model_data.max_mass_ts = value;
                OnPropertyChanged("MaxMassaTS");
            }
        }

        public bool IsRegularPerevoz // Используется в регулярных перевозках
        {
            get
            {
                return model_data.is_regul_perevoz;
            }
            set
            {
                model_data.is_regul_perevoz = value;
                OnPropertyChanged("IsRegularPerevoz");
            }
        }

        public int CountPassangers // Количество пассажирских мест
        {
            get
            {
                return model_data.count_pasangers;
            }
            set
            {
                if (!(SelectedCategoriaTS.id_categoria_ts == 3 && CountPassangers == 0))
                {
                    ClearErrors("CountPassangers");
                }
                model_data.count_pasangers = value;
                OnPropertyChanged("CountPassangers");
            }
        }
        public bool IsWarinig // "Особый" случай
        {
            get
            {
                return model_data.is_warning;
            }
            set
            {
                model_data.is_warning = value;
                OnPropertyChanged("IsWarinig");
            }
        }

        private string _formula_rascheta { get; set; } = ""; // Служебная переменная хранит формулу расчета

        public string ResultRascheta // Представление результатов расчета
        {
            get
            {
                return _formula_rascheta;
            }
            set
            {
                _formula_rascheta = value;
                OnPropertyChanged("ResultRascheta");
            }
        }

        public string StrahPrem { get; set; } = ""; // Результирующая страховая премия

        // Обработчик команд       
        public ICommand CommandCalc // Команда запуска расчетов
        {
            get { return new DelegateCommand(CalcAllSumm); }
        }

        public ICommand CommandReset // Команда сброса показателей
        {
            get { return new DelegateCommand(ResetData); }
        }

        public ICommand CommandAddDriver // Команда добавления водителя
        {
            get { return new DelegateCommand(AddDriver); }
        }

        public ICommand CommandDelDriver // Команда удаления водителя
        {
            get { return new DelegateCommand(DelDriver); }
        }

        // Реализация команд
        private void CalcAllSumm() // Процедура запускает процесс калькуляции стоимости полиса
        {
            bool is_errors = false; // Наличие ошибок
            // Для определенных категорий ТС должны быть заполнены определенные поля
            if (PowerTS == 0) // Для категории B - мощность
            {
                // добавляем новый список ошибок по свойству
                List<string> text_errors = new List<string>();
                text_errors.Add("Укажите мощность ТС");
                SetErrors("PowerTS", text_errors);
                is_errors = true;
            }
            if (MaxMassaTS == 0) // Для категории С - разрешенная масса
            {
                // добавляем новый список ошибок по свойству
                List<string> text_errors = new List<string>();
                text_errors.Add("Укажите массу ТС");
                SetErrors("MaxMassaTS", text_errors);
                is_errors = true;
            }
            if (CountPassangers == 0) // Для категории D - количество пассажирских мест
            {
                // добавляем новый список ошибок по свойству
                List<string> text_errors = new List<string>();
                text_errors.Add("Укажите количество пассажирских мест ТС");
                SetErrors("CountPassangers", text_errors);
                is_errors = true;
            }
            if (ListSubRegions.Count == 0 && EnableSubRegions) // // Если не выбрано уточнения территории
            {
                // добавляем новый список ошибок по свойству
                List<string> text_errors = new List<string>();
                text_errors.Add("Необходимо выбрать уточнение территории");
                SetErrors("ListSubRegions", text_errors);
                is_errors = true;
            }
            if (PowerTS != 0 && MaxMassaTS != 0 && CountPassangers != 0 && is_errors == false)
            {
                //(is_errors) { return; } // Если есть ошибки, тогда выходим
                model_data.Drivers.Clear(); // Заполняем водителей, но перед этим очищаем
                foreach (DB_Driver tmp_driver in Drivers)
                {
                    model_data.Drivers.Add(tmp_driver);
                }
                decimal result = model_data.CalcStrahPrem(); // Результат вычисления 
                var numberFormatInfo = new System.Globalization.CultureInfo("ru-Ru", false).NumberFormat; // Форматированный вывод с разделением на тысячи
                StrahPrem = result.ToString("N", numberFormatInfo).Replace(",", ".") + "  руб.";
                OnPropertyChanged("StrahPrem");
                ResultRascheta = model_data.GetResultFormula(); // Формула расчета с показателями
            }
        }

        private void ResetData() // Процедура сбрасывает текущие показатели
        {
            model_data.ResetSetters();
            ResultRascheta = "";
            OnPropertyChanged("FioStrah");
            OnPropertyChanged("ResultRascheta");
            OnPropertyChanged("SelectedUrFizLicoIndex");
            OnPropertyChanged("IsForegin");
            OnPropertyChanged("IsDriverLimit");
            OnPropertyChanged("CountAge");
            OnPropertyChanged("CountStage");
            OnPropertyChanged("NewKbm");
            OnPropertyChanged("IsTaxi");
            OnPropertyChanged("IsPricep");
            OnPropertyChanged("SelectedCategoriaTS");
            OnPropertyChanged("IsTransit");
            OnPropertyChanged("PowerTS");
            OnPropertyChanged("MaxMassaTS");
            OnPropertyChanged("IsWarinig");
            OnPropertyChanged("IsRegularPerevoz");
            OnPropertyChanged("CountPassangers");
            OnPropertyChanged("SelectedPeriodUse");
            OnPropertyChanged("SelectedPeriodStrah");
            SelectedRegion = get_selected_region(1);
            OnPropertyChanged("SelectedRegion");
            StrahPrem = "0.00";
            OnPropertyChanged("StrahPrem");
            model_data.Drivers.Clear();
            Drivers.Clear();
            OnPropertyChanged("Drivers");
            BlockKbm = true;
            OnPropertyChanged("BlockKbm");
            BlockPeriodUse = true;
            OnPropertyChanged("BlockPeriodUse");
            BlockDriverLimit = true;
            OnPropertyChanged("BlockDriverLimit");
            EnableRegions = true;
            OnPropertyChanged("EnableRegions");
            EnableSubRegions = true;
            OnPropertyChanged("EnableSubRegions");
            ClearErrors("ListSubRegions");
            OnPropertyChanged("ListSubRegions");
        }

        private void AddDriver() // Процедура добавления водителя в список
        {
            if (Drivers.Count() < 4)
            {
                int a = Drivers.Count + 1;
                DB_Driver tmp_driver = new DB_Driver();
                tmp_driver.NameDriver = "В" + a;
                Drivers.Add(tmp_driver);
                OnPropertyChanged("Drivers");
            }
        }

        private void DelDriver() // Процедура удаления водителя из списка
        {
            if (SelectedDriver != null)
            {
                Drivers.Remove(SelectedDriver);
            }
            OnPropertyChanged("Drivers");
        }

        public event PropertyChangedEventHandler PropertyChanged; // Реализация интерфейса INotifyPropertyChanged
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        // Реализация интерфейса INotifyDataErrorInfo
        private Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>(); // Список ошибок, которые происходили

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged; // Событие для вызова ошибки

        public bool HasErrors // Возвращает true, если имеются ошибки
        {
            get
            {
                return (errors.Count > 0); // Имеются ли ошибки
            }
        }

        public IEnumerable GetErrors(string propertyName) // Функция возвращает перечисление коллекции ошибок по свойству
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return errors.Values; // Предоставить всю коллекцию ошибок
            }
            else
            {
                // Предоставить коллекцию ошибок для запрашиваемого свойства
                if (errors.ContainsKey(propertyName))
                {
                    return errors[propertyName];
                }
                else
                {
                    return null;
                }
            }
        }

        private void SetErrors(string propertyName, List<string> propertyErrors) // Обработчик добавления ошибок
        {

            errors.Remove(propertyName); // Очищаем ошибки, которые существуют по этому свойству
            errors.Add(propertyName, propertyErrors); // Добавляем в список причину ошибки и необходимый элемент
            if (ErrorsChanged != null) // Вызываем обработчик
            {
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
            }
            errors.Remove(propertyName); // Очищаем ошибки, которые существуют по этому свойству
        }

        private void ClearErrors(string propertyName) // Обработчик удаления ошибок
        {
            errors.Remove(propertyName); // Очищаем ошибки, которые существуют по этому свойству
            if (ErrorsChanged != null) // Вызываем обработчик
            {
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        // Конструктор класса
        public ViewModelMainWindow()
        {
            // Инициализируем модель данный
            model_data = new Calculation();

            // Получаем перечисление юр.физ. лицо
            DictUrFizLico = model_data.GetUrFizLicoList();
            OnPropertyChanged("DictUrFizLico");

            // Получаем список категорий ТС
            ListCategoriaTs = model_data.GetCategoriaTs();
            OnPropertyChanged("ListCategoriaTs");

            // Получаем список сроков страхования ТС
            ListPeriodStrah = model_data.GetPeriodStrahTs();
            OnPropertyChanged("ListPeriodStrah");

            // Получаем список периодов использования ТС
            ListPeriodUse = model_data.GetPeriodUseTs();
            OnPropertyChanged("ListPeriodUse");

            // Получаем список регионов
            ListRegions = model_data.GetRegions();
            OnPropertyChanged("ListRegions");
            SelectedRegion = get_selected_region(1);
            OnPropertyChanged("SelectedRegion");

            // Получаем список КБМ
            ListKbm = model_data.GetKbms();
            OnPropertyChanged("ListKbm");
        }
    }
}
