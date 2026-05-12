using Microsoft.VisualStudio.TestTools.UnitTesting;
using Soul_Talk.Model;

namespace UseCase1_Test
{
    [TestClass] 
    public class Test1
    {
        [TestMethod] 
        //Test for at teste om en Guest med borgeroplysninger er initialiseret korrekt. 
        public void Constructor_ShouldInitializePropertiesCorrectly_WhenValidDataIsProvided()
        {
            // Arrange
            int expectedId = 1;
            string expectedName = "Mads Jensen";
            string expectedPhone = "12345678";
            string expectedEmail = "mads@example.dk";
            string expectedCpr = "123456-7890";
            string expectedWorkStatus = "Ledig";
            string expectedWorkType = "Kontor";
            string expectedConsent = "Givet";
            string expectedStatus = "Aktiv";
            string expectedNotes = "Ingen særlige hensyn";

            // Act
            var citizen = new Citizen(
                expectedId,
                expectedName,
                expectedPhone,
                expectedEmail,
                expectedCpr,
                expectedWorkStatus,
                expectedWorkType,
                expectedConsent,
                expectedStatus,
                expectedNotes,
                1
            );

            // Assert
            Assert.AreEqual(expectedId, citizen.CitizenId);
            Assert.AreEqual(expectedName, citizen.Name);
            Assert.AreEqual(expectedCpr, citizen.CprNumber);
            Assert.AreEqual(expectedWorkStatus, citizen.WorkStatus);
            Assert.AreEqual(expectedStatus, citizen.CurrentStatus);

        }
    }
}