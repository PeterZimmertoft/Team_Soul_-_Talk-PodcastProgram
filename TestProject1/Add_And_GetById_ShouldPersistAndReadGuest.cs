using Microsoft.VisualStudio.TestTools.UnitTesting;
using Soul_Talk.Model;
using Soul_Talk.Persistence__Repositories_;

[TestClass]
public class GuestRepositoryTests
{
    
    private readonly string _connectionString = "Server=localhost;Database=SoulTalkDB;Trusted_Connection=True;TrustServerCertificate=True;";
    private GuestRepository _repository;

    [TestInitialize]
    public void Setup()
    {
        _repository = new GuestRepository(_connectionString);
    }

    [TestMethod]
    public void Add_And_GetById_ShouldPersistAndReadGuest()
    {
        // Arrange

        var testGuest = new Guest
        {
            Name = "Senan Bradley",
            Phone = "1234567890",
            Email = "Senan@Bradley.com",
        };

        // Act

            int newGuestId = _repository.Add(testGuest);

            Guest retrievedGuest = _repository.GetById(newGuestId);

        // Assert

            Assert.IsNotNull(retrievedGuest);
            Assert.AreEqual(testGuest.Name, retrievedGuest.Name);
            Assert.AreEqual(testGuest.Phone, retrievedGuest.Phone);
            Assert.AreEqual(testGuest.Email, retrievedGuest.Email);
    }
}