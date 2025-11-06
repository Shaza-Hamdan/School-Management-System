using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trial.DTO;

[ApiController]
[Route("api/[controller]")]
public class HomeworkTeacherController : ControllerBase
{
    private readonly IHomeworkTeacherService homeworkservice;

    public HomeworkTeacherController(IHomeworkTeacherService homeworkService)
    {
        homeworkservice = homeworkService;
    }

    [Authorize(Roles = "Teacher,Admin")]
    [HttpGet("GetAll Homeworks")]
    public async Task<IEnumerable<HomeworkTDTO>> GetHomeworks()
    {
        return await homeworkservice.GetHomeworksAsync();
    }

    [Authorize(Roles = "Teacher,Admin")]
    [HttpGet("Get a Homework")]
    public async Task<ActionResult<HomeworkTDTO>> GetHomework(int id)
    {
        var homework = await homeworkservice.GetHomeworkByIdAsync(id);
        if (homework == null)
        {
            return NotFound();
        }
        return homework;
    }

    [Authorize(Roles = "Teacher,Admin")]
    [HttpPost("Add a homework")]
    public async Task<string> AddHomework(AddHomeworkTDTO addHomeworkDto)
    {

        var homework = await homeworkservice.AddHomeworkAsync(addHomeworkDto);

        if (homework == null)
        {
            return "Homework could not be created.";
        }
        else
        {
            return "Homework added successfully";
        }

    }

    [Authorize(Roles = "Teacher,Admin")]
    [HttpPut("Edit an existing Homework")]
    public async Task<string> UpdateHomework(ModifyHomeworkTDTO modifyHomeworkDto)
    {
        try
        {
            var result = await homeworkservice.UpdateHomeworkAsync(modifyHomeworkDto);
            if (!result)
            {
                return "Homework has not been edited successfully";
            }

            return "Homework has been edited successfully";
        }
        catch (Exception)
        {
            return $"An error occurred while editing homework, please try again";
        }
    }


    [Authorize(Roles = "Teacher,Admin")]
    [HttpPost("cleanup expired Homeworks")]
    public async Task<IActionResult> CleanupExpiredHomeworks()
    {
        var deleted = await homeworkservice.CleanupExpiredHomeworksAsync();
        if (deleted)
        {
            return Ok("Expired homework has been deleted.");
        }
        else
        {
            return Ok("No expired homework has been found.");
        }
    }
}
