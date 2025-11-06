using Trial.DTO;
using TRIAL.Persistence.entity;

namespace TRIAL.Services
{
    public interface IStudentHomeworkService
    {
        //Task<StudentHomeworkDTO> SubmitHomeworkAsync(AddStudentHomeworkDTO addStudentHomeworkDto);
        Task<HomeworkStudent> GetStudentHomeworkByIdAsync(int studentId);
        Task<string> UpdateHomeworkFileAsync(int homeworkTId, int registrationId, IFormFile newFile);
        Task<bool> DeleteHomework(int homeworkTId, int registrationId);
        Task<string> UploadHomeworkFileAsync(int homeworkTId, int registrationId, IFormFile file);

    }
}