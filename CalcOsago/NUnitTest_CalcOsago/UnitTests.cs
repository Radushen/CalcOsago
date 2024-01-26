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



        [Test]
        public void TestCalculation() // Проверяет работу метода по возвращению значения SelectedUrFizLicoIndex
        {
            // Arrange
            var calculation = new Calculation(); // Creating an instance of your class
            
            var inputFormula = new Dictionary<string, double>
            {
                { "Tb:", 0.0 },
                { "Kt:", 0.0 },
                { "Kbm:", 0.0 },
                { "Kvs:", 0.0 },
                { "Ko:", 0.0 },
                { "Km:", 100.0 },
                { "Ks:", 0.0 },
                { "Kn:", 0.0 },
                { "Kp:", 0.0 },
                { "Kpr:", 0.0 }
            };

            // Act
            var result = calculation.FillFormulaResult(inputFormula);

            // Assert
            Assert.IsNotNull(result);

            Assert.IsTrue(result.ContainsKey("Tb:"));
            Assert.IsTrue(result.ContainsKey("Kt:"));
            Assert.IsTrue(result.ContainsKey("Kbm:"));
            Assert.IsTrue(result.ContainsKey("Kvs:"));
            Assert.IsTrue(result.ContainsKey("Ko:"));
            Assert.IsTrue(result.ContainsKey("Km:"));
            Assert.IsTrue(result.ContainsKey("Ks:"));
            Assert.IsTrue(result.ContainsKey("Kn:"));
            Assert.IsTrue(result.ContainsKey("Kp:"));
            Assert.IsTrue(result.ContainsKey("Kpr:"));

            Assert.That(result["Tb:"], Is.EqualTo(0));
            Assert.That(result["Kt:"], Is.EqualTo(0));
            Assert.That(result["Kbm:"], Is.EqualTo(1));
            Assert.That(result["Kvs:"], Is.EqualTo(1));
            Assert.That(result["Ko:"], Is.EqualTo(1.87));
            Assert.That(result["Ks:"], Is.EqualTo(1));
            Assert.That(result["Kn:"], Is.EqualTo(1));
            Assert.That(result["Kp:"], Is.EqualTo(0.2));
            Assert.That(result["Kpr:"], Is.EqualTo(1));
        }
    }
}