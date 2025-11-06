using Microsoft.AspNetCore.Mvc;
using Trial.DTO;


namespace TRIAL.Services
{

    public interface IMarksService
    {
        Task<string> AddMarks(AddMark dto);
        Task<object?> GetTheMark(GetMark dto);
    }

}