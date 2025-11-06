using Microsoft.AspNetCore.Mvc;
using TRIAL.Services;
using TRIAL.Persistence.Repository;
using Trial.DTO;
using Microsoft.AspNetCore.Authorization;

namespace TRIAL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentHomeworkController : ControllerBase
    {
        private readonly IStudentHomeworkService studenthomeworkService;
        private readonly AppDBContext appdbContext;
        public StudentHomeworkController(IStudentHomeworkService studentHomeworkservice)
        {
            studenthomeworkService = studentHomeworkservice;
        }

        [Authorize(Roles = "Student")]
        [HttpPost("upload a homework")]
        public async Task<IActionResult> UploadHomework(int homeworkTId, int registrationId, IFormFile file)
        {
            try
            {
                var result = await studenthomeworkService.UploadHomeworkFileAsync(homeworkTId, registrationId, file);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Teacher,Admin,Student")]
        [HttpGet("Get/{RegistrationId}")]
        public async Task<IActionResult> GetStudentHomeworkById(int RegistrationId)
        {
            try
            {
                var homework = await studenthomeworkService.GetStudentHomeworkByIdAsync(RegistrationId);
                if (homework == null)
                {
                    return NotFound("Homework not found.");
                }

                // Optionally, return the file itself if we want the teacher to download it
                var filePath = homework.FilePath;
                var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);//Storing the file in a byte array
                return File(fileBytes, "application/octet-stream", Path.GetFileName(filePath));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Student")]
        [HttpPut("update a homework")]
        public async Task<IActionResult> UpdateHomework(int homeworkTId, int registrationId, IFormFile newFile)
        {
            if (newFile == null || newFile.Length == 0)
            {
                return BadRequest("A valid file must be uploaded.");
            }

            try
            {
                var result = await studenthomeworkService.UpdateHomeworkFileAsync(homeworkTId, registrationId, newFile);
                return Ok(new { message = result });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); // File not uploaded
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message); // Record not found
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the homework file.");
            }
        }

        [Authorize(Roles = "Admin,Student")]
        [HttpDelete("Delete a homework")]
        public async Task<IActionResult> DeleteHomework(DeleteHomework deleteHomework)
        {
            bool result = await studenthomeworkService.DeleteHomework(
                deleteHomework.homeworkTId,
                deleteHomework.RegistrationId
            );

            if (!result)
            {
                return NotFound(new { Message = "Homework not found or you do not have permission to delete it." });
            }

            return Ok(new { Message = "Homework deleted successfully." });
        }
    }
}