using NUnit.Framework;
using System.Data;
using CalcOsago;

namespace NUnitTest_CalcOsago
{
    public class UnitTests
    {
        [Test]
        public void TestWorkWith_DB() // Тестирует корректное подключение к базе
        {
            // Arrange
            WorkWith_DB workWithDb = new WorkWith_DB();

            // Act
            var connectionState = workWithDb.connect.State;

            // Assert
            Assert.That(connectionState, Is.EqualTo(System.Data.ConnectionState.Open));
        }

        [Test]
        public void TestMainWindowView_1() // Проверяет работу метода по возвращению значения FioStrah
        {
            ViewModelMainWindow viewModel = new ViewModelMainWindow();

            // Arrange
            viewModel.model_data.fio_strah = "John Doe";

            // Act
            string result = viewModel.FioStrah;

            // Assert
            Assert.That(result, Is.EqualTo("John Doe"));
        }

        [Test]
        public void TestMainWindowView_2() // Проверяет работу метода по возвращению значения SelectedUrFizLicoIndex
        {
            ViewModelMainWindow viewModel = new ViewModelMainWindow();

            // Arrange
            viewModel.model_data.ind_ur_fiz_lico = 1;
            viewModel.DictUrFizLico.Add(new DB_Person { id_cl = 1 });
            viewModel.DictUrFizLico.Add(new DB_Person { id_cl = 2 });

            // Act
            var result = viewModel.SelectedUrFizLicoIndex;

            // Assert
            Assert.That(result, Is.EqualTo(viewModel.DictUrFizLico[0]));
        }
    }
}