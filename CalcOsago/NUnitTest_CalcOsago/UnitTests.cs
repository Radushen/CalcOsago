using NUnit.Framework;
using System.Data;
using CalcOsago;

namespace NUnitTest_CalcOsago
{
    public class UnitTests
    {
        [Test]
        public void TestWorkWith_DB()
        {
            // Arrange
            WorkWith_DB workWithDb = new WorkWith_DB();

            // Act
            var connectionState = workWithDb.connect.State;

            // Assert
            Assert.That(connectionState, Is.EqualTo(System.Data.ConnectionState.Open));
        }
    }
}