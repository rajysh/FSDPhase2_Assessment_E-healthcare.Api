using Ehealthcare.Api.Controllers;
using Ehealthcare.Api;
using NUnit.Framework;
using Moq;
using EHealthcare.Entities;
using ProjectManagement.Data;
using Moq.AutoMock;
using ProjectManagement.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ProjectManagement.Api.Controllers;

namespace E_healthcare.ApiTests.Controllers
{
    [TestFixture]
    public class ProductControllerTest
    {
        private Mock<IBaseRepository<User>> UserRepository = new Mock<IBaseRepository<User>>();
        private Mock<IBaseRepository<Category>> CategoryRepository = new Mock<IBaseRepository<Category>>();
        private ProductController productControllerTest;
        private Mock<IBaseRepository<Product>> BaseRepository = new Mock<IBaseRepository<Product>>();

        [Test]
        public void AddMedicineTest()
        {
            productControllerTest = new ProductController(CategoryRepository.Object);
            var result = productControllerTest.AddMedicine(new Product()
            {
                Name = "Nutriosys Isabgol",
                CompanyName = "Company1",
                Price = 525,
                ImageUrl = "https://m.media-amazon.com/images/I/51inZITDWAL._AC_UL320_.jpg"
            });

            Assert.AreEqual(result, true);
        }        
    }    
}