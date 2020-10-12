using NUnit.Framework;
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
            var userController = new UserController();

            // Act
            var result = userController.Get();

            // Assert
            // If there's complex logic defined in controller, then
            // verify the logic. 
            // Do not verify logic of UserHandler here.
            Assert.That(result != null);
        }
    }
}
