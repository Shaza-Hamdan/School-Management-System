using System.Drawing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trial.DTO;
using TRIAL.Services;

namespace TRIAL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarksController : ControllerBase
    {
        private readonly IMarksService marksService;

        public MarksController(IMarksService Marksservice)
        {
            marksService = Marksservice;
        }
        [Authorize(Roles = "Teacher,Admin")]
        [HttpPost("Add-mark")]
        public async Task<IActionResult> AddNewMarks(AddMark dto)
        {
            try
            {
                var result = await marksService.AddMarks(dto);
                if (result == "Marks added successfully.")
                    return Ok(new { Message = result });
                else
                    return BadRequest(new { Message = result });
            }
            catch
            {
                return StatusCode(500, new { Message = "An unexpected error occurred while adding marks." });
            }
        }
        [Authorize(Roles = "Teacher,Admin,Student")]
        [HttpGet("Get-mark")]
        public async Task<IActionResult> Getmark([FromQuery] GetMark dto)
        {
            var result = await marksService.GetTheMark(dto);
            if (result is string errorMessage)
                return NotFound(new { Message = errorMessage });

            return Ok(result);

        }

    }
}