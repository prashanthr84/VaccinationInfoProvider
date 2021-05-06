using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VaccinationInfoProvider.UserManagement;

namespace VaccinationInfoProvider.Controllers {

    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase {
        private readonly IUserManagement userManagement;

        public UserController(IUserManagement userManagement) {
            this.userManagement = userManagement;
        }

        [HttpPost("register")]
        public IActionResult Put(
            [FromBody] User user
        ) {

            user = userManagement.Register(user);
            return Created("NotAllowed", user);
        }
    }
}
