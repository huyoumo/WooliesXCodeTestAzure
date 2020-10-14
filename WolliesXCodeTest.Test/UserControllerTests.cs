using NSubstitute;
using NUnit.Framework;
using WooliesXCodeTest.BusinessLogic;
using WooliesXCodeTest.BusinessLogic.Models;
using WooliesXCodeTestWebAPI.Controllers;

namespace WooliesXCodeTest.Test
{
    [TestFixture]
    public class UserControllerTests
    {
        [Test]
        public void Get_Should_ReturnAUser() 
        {
            // Arrange
            var userHandler = Substitute.For<IUserHandler>();
            userHandler.GetUser().Returns(new User());

            var userController = new UserController(userHandler);

            // Act
            var result = userController.Get();

            // Assert
            // If there's complex logic defined in controller, then
            // verify the logic. 
            // Do not verify logic of UserHandler here.
            Assert.That(result != null);
            userHandler.Received(1).GetUser();
        }
    }
}
