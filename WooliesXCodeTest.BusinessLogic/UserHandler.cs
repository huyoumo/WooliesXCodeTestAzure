using WooliesXCodeTest.BusinessLogic.Models;

namespace WooliesXCodeTest.BusinessLogic
{
    public class UserHandler: IUserHandler
    {
        public User GetUser() {
            return new User()
            {
                Name = "David Hu",
                Token = "1234-455662-22233333-3333"
            };
        }
    }
}
