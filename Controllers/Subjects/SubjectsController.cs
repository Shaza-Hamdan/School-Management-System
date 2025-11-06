using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trial.DTO;
using TRIAL.Services;

namespace TRIAL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectsService subjectsret;

        public SubjectsController(ISubjectsService SubjectsRet)
        {
            subjectsret = SubjectsRet;
        }
        [Authorize(Roles = "Teacher,Admin,Student")]
        [HttpGet("Get")]
        public async Task<IActionResult> GetSubjects()
        {
            var subjects = await subjectsret.GetSubjectsAsync();
            return Ok(subjects);
        }
        [Authorize(Roles = "Teacher,Admin,Student")]
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetSubjectDetail(int id)
        {
            var subjectDetail = await subjectsret.GetSubjectDetailAsync(id);
            if (subjectDetail == null)
            {
                return NotFound();
            }

            return Ok(subjectDetail);
        }

        [Authorize(Roles = "Teacher,Admin")]
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteSubject(int subjectId)
        {
            bool result = await subjectsret.DeleteSubjectAsync(subjectId);

            if (!result)
            {
                return NotFound(new { Message = "Subject not found." });
            }

            return Ok(new { Message = "Subject deleted successfully." });
        }

        [Authorize(Roles = "Teacher,Admin")]
        [HttpPost("Add")]
        public async Task<IActionResult> AddNewSubject([FromBody] AddNewSubject subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await subjectsret.AddNewSubject(subject);
                if (result == "Successfully added the new subject.")
                {
                    return Ok(new { message = result });
                }
                else
                {
                    return BadRequest(new { message = result });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Couldn't add new subject", error = ex.Message });
            }
        }

        [Authorize(Roles = "Teacher,Admin")]
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateSubject([FromBody] UpdateSubject updateSubject)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await subjectsret.UpdateSubject(updateSubject);

            if (!result)
            {
                return BadRequest(new { message = "Could not update the subject." });
            }

            return Ok(new { message = "Successfully updated the subject." });
        }
    }
}