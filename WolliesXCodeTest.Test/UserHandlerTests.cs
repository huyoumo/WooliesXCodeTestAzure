using NUnit.Framework;
using WooliesXCodeTest.BusinessLogic;

namespace WolliesXCodeTest.Test
{
    public class UserHandlerTests
    {
        [Test]
        public void GetUser_Should_ReturnAUser()
        {
            // Arrange
            var userHandler = new UserHandler();

            // Act
            var result = userHandler.GetUser();

            // Assert
            Assert.That(result.Name.Equals("David Hu"));
            Assert.That(result.Token.Equals("1234-455662-22233333-3333"));
        }
    }
}