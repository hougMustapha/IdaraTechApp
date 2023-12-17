using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdaraTechApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RCPracticeController : ControllerBase
    {
        [HttpGet("public")]
        public IActionResult Public()
        {
            return Ok("public");
        }

        #region Roles

        [HttpGet("superAdmin-role")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult SuperAdminRole()
        {
            return Ok("super admin role");
        }

        [HttpGet("admin-role")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminRole()
        {
            return Ok("admin role");
        }

        [HttpGet("manager-role")]
        [Authorize(Roles = "Manager")]
        public IActionResult ManagerRole()
        {
            return Ok("manager role");
        }

        [HttpGet("customer-role")]
        [Authorize(Roles = "Customer")]
        public IActionResult CustomerRole()
        {
            return Ok("customer role");
        }

        [HttpGet("citoyen-role")]
        [Authorize(Roles = "Citoyen")]
        public IActionResult CitizenRole()
        {
            return Ok("citoyen role");
        }

        [HttpGet("admin-or-manager-role")]
        [Authorize(Roles = "Admin, Manager")]
        public IActionResult AdminOrManagerRole()
        {
            return Ok("admin or manager role");
        }

        [HttpGet("admin-or-customer-role")]
        [Authorize(Roles = "Admin, Customer")]
        public IActionResult AdminOrCustomerRole()
        {
            return Ok("admin or customer role");
        }

        [HttpGet("admin-or-citoyen-role")]
        [Authorize(Roles = "Admin, Citoyen")]
        public IActionResult AdminOrCitoyenRole()
        {
            return Ok("admin or citoyen role");
        }

        #endregion

        #region Policy

        [HttpGet("superAdmin-policy")]
        [Authorize(policy: "SuperAdminPolicy")]
        public IActionResult SuperAdminPolicy()
        {
            return Ok("super admin policy");
        }

        [HttpGet("admin-policy")]
        [Authorize(policy: "AdminPolicy")]
        public IActionResult AdminPolicy()
        {
            return Ok("admin policy");
        }

        [HttpGet("manager-policy")]
        [Authorize(policy: "ManagerPolicy")]
        public IActionResult ManagerPolicy()
        {
            return Ok("manager policy");
        }

        [HttpGet("customer-policy")]
        [Authorize(policy: "CustomerPolicy")]
        public IActionResult CustomerPolicy()
        {
            return Ok("customer policy");
        }

        [HttpGet("citizen-policy")]
        [Authorize(policy: "CitizenPolicy")]
        public IActionResult CitizenPolicy()
        {
            return Ok("citoyen policy");
        }

        [HttpGet("admin-or-manager-policy")]
        [Authorize(policy: "AdminOrManagerPolicy")]
        public IActionResult AdminOrManagerPolicy()
        {
            return Ok("admin or manager policy");
        }

        [HttpGet("admin-and-manager-policy")]
        [Authorize(policy: "AdminAndManagerPolicy")]
        public IActionResult AdminAndManagerPolicy()
        {
            return Ok("admin and manager policy\"");
        }

        [HttpGet("all-role-policy")]
        [Authorize(policy: "AllRolePolicy")]
        public IActionResult AllRolePolicy()
        {
            return Ok("all roles policy");
        }

        #endregion

        #region Claim Policy

        [HttpGet("admin-email-policy")]
        [Authorize(policy: "AdminEmailPolicy")]
        public IActionResult AdminEmailPolicy()
        {
            return Ok("admin email policy");
        }

        [HttpGet("hanane-surname-policy")]
        [Authorize(policy: "HananeSurnamePolicy")]
        public IActionResult HananeSurnamePolicy()
        {
            return Ok("hanane surname policy");
        }

        [HttpGet("manager-email-and-nacer-surname-policy")]
        [Authorize(policy: "ManagerEmailAndNacerSurnamePolicy")]
        public IActionResult ManagerEmailAndNacerSurnamePolicy()
        {
            return Ok("manager email and nacer surname policy");
        }

        [HttpGet("ship-policy")]
        [Authorize(policy: "SHIPPolicy")]
        public IActionResult SHIPPolicy()
        {
            return Ok("ship policy");
        }


        #endregion
    }
}
