using BasicJwtAuth.App_Repo;
using BasicJwtAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasicJwtAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IJwtManagerRepo _jwtManagerRepo;

        public UsersController(IJwtManagerRepo jwtManagerRepo)
        {
            _jwtManagerRepo = jwtManagerRepo;
        }

        [HttpGet]
        [Route("userlist")]
        public List<string> Get() { 
        var users = new List<string>
        {
            "Margaret Lunani",
            "Natasha Simiyu"
        };
            return users;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(Users userdata) {

            var token = _jwtManagerRepo.Authenticate(userdata);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}
