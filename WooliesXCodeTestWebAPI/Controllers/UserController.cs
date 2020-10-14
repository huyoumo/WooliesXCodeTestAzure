using Microsoft.AspNetCore.Mvc;
using WooliesXCodeTestWebAPI.Models;
using WooliesXCodeTest.BusinessLogic;
using Mapster;

namespace WooliesXCodeTestWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Public
        public UserController(IUserHandler userHandler) 
        {
            _userHandler = userHandler;
        }

        [HttpGet]
        public User Get()
        {
            return _userHandler.GetUser().Adapt<User>();
        }
        #endregion

        #region Private
        private IUserHandler _userHandler;
        #endregion
    }
}
