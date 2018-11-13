using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.BusinessServices;
using LibertyWebAPI.BusinessServices.Contracts;
using LibertyWebAPI.DataModel.Contracts;
using LibertyWebAPI.DataModel.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data;

namespace LibertyWebAPI.UnitTest
{
    [TestClass]
    public class InstitutionServiceUnitTest
    {
        [TestMethod]
        public void InstitutionService_RepositoryGetInstitution_WasCalled()
        {
            //Arrange
            var repository = new Mock<IInstitutionRepository>();
            repository
                .Setup(r => r.GetInstitution(new DTO.Institution.InstitutionRequestDTO()));
            var service = new InstitutionService(repository.Object);

            //Act
            var institution = service.GetInstitution(new DTO.Institution.InstitutionRequestDTO());

            //Assert
            repository.VerifyAll();
        }

        [TestMethod]
        public void InstitutionService_RepositoryReturnsNull_ReturnNullInstitution()
        {
            ////Arrange
            //var mockRepository = new Mock<IInstitutionRepository>();

            //mockRepository
            //    .Setup(r => r.GetInstitution(It.IsAny<string>(), It.IsAny<string>()))
            //    .Returns(() => null);

            //var service = new InstitutionService(mockRepository.Object);

            ////Act
            //var institution = service.GetInstitution(string.Empty, string.Empty);

            ////Asset
            //Assert.IsNull(institution);
        }

        [TestMethod]
        public void InstitutionService_WithValidRoutingAndAccount_ReturnNullInstitution()
        {
            ////Arrange
            //var mockRepository = new Mock<IInstitutionRepository>();
            //var mockService = new Mock<IInstitutionService>(mockRepository.Object);
            //var mockBaseRepo = new Mock<BaseRepository<Institution>>();

            //mockBaseRepo.Setup(b => b.PopulateRecord(It.IsAny<IDataReader>()));

            //IInstitutionRepository repository = new RepositoryReturnNullInstitution();
            //var routing = "0123456789";
            //var account = "0987654321";
            //var service = new InstitutionService(repository);

            ////Act
            //var institution = service.GetInstitution(routing, account);

            ////Assert
            //Assert.AreEqual(routing, institution.transitRouting, "The transitRouting returned should match what was passed in");
            //Assert.AreEqual(account, institution.account, "The account returned should match what was passed in");
        }
    }
}
