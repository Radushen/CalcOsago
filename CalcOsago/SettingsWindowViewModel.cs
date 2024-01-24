using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcOSAGO
{
	public class SettingsWindowViewModel : INotifyPropertyChanged
	{
		public DataTable DataTableBaseTariff { get; set; }

		// реализация изменения свойства
		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}


		// КОНСТРУКТОР КЛАССА
		public SettingsWindowViewModel(DataTable tmp_dt)
		{
			DataTableBaseTariff = tmp_dt;
		}
	}
}
