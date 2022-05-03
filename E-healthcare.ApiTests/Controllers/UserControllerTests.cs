using Ehealthcare.Api.Controllers;
using Ehealthcare.Api;
using NUnit.Framework;
using Moq;
using EHealthcare.Entities;
using ProjectManagement.Data;

namespace E_healthcare.ApiTests.Controllers
{
    [TestFixture]
    public class UserControllerTest
    {
        private Mock<IBaseRepository<User>> UserRepository = new Mock<IBaseRepository<User>>();
        private UserController userControllerTest;
        private Mock<IBaseRepository<User>> BaseRepository = new Mock<IBaseRepository<User>>();  

        [Test]
        public void UserRegistrationTest()
        {
            userControllerTest = new UserController(UserRepository.Object);
            var result = userControllerTest.Register(new User() 
            { 
                Email = "usertest111@test.com", 
                Password = "Password1", 
                FirstName = "Raj", 
                LastName = "Babu", 
                IsAdmin = true });
            Assert.AreEqual(result, true);
        }
        
        [Test]
        public void UserLoginTest()
        {
            userControllerTest = new UserController(UserRepository.Object);
            var authUserModel = new AuthUserModel
            {
                ID = 1,
                FirstName = "Test",
                LastName = "",
                AccessToken = "test",
                IsAdmin = false
            };
            var result = userControllerTest.LoginUser(new Ehealthcare.Entities.Dto.LoginDto() 
            { 
                Email = "testuser1@test.com", 
                Password = "Password1", 
                Type = "admin" 
            });

            Assert.IsNotEmpty(result.ToString());
        }
    }
}
