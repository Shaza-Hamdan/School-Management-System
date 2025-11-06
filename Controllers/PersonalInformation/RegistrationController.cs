using Microsoft.AspNetCore.Mvc;
using Trial.DTO;
using TRIAL.Services;


namespace TRIAL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService registrationservice;

        public RegistrationController(IRegistrationService registrationService)
        {
            registrationservice = registrationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateNewAccount request)
        {
            try
            {
                var result = await registrationservice.Register(request);

                if (result == "EmailExists")
                {
                    return BadRequest(new { message = "The email address is already registered. Please use a different email." });
                }

                if (result == "Success")
                {
                    return Ok(new { message = "New user has been added successfully." });
                }

                return BadRequest(new { message = "Registration failed due to an unknown reason." });
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = registrationservice.Login(account);
            if (result == "Login Successful")
            {
                return Ok(result);
            }
            else if (result == "Invalid Email or Password")
            {
                return Unauthorized(result);
            }
            else
            {
                return StatusCode(500, result);
            }

        }

        [HttpPost("generate-password-reset-token")]
        public async Task<IActionResult> GeneratePasswordResetToken([FromBody] PasswordResetRequest request)
        {
            var result = await registrationservice.GeneratePasswordResetTokenAsync(request.Email);
            if (result == "Password reset token generated.")
            {
                return Ok(new { Message = result });
            }
            return BadRequest(new { Message = result });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest model)
        {
            var result = await registrationservice.ResetPasswordAsync(model);
            if (result == "Password reset successful.")
            {
                return Ok(new { Message = result });
            }
            return BadRequest(new { Message = result });
        }


    }
}