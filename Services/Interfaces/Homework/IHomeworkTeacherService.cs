using Trial.DTO;

public interface IHomeworkTeacherService
{
    Task<IEnumerable<HomeworkTDTO>> GetHomeworksAsync();
    Task<HomeworkTDTO> GetHomeworkByIdAsync(int id);
    Task<string> AddHomeworkAsync(AddHomeworkTDTO addHomeworkDto);
    Task<bool> UpdateHomeworkAsync(ModifyHomeworkTDTO modifyHomeworkDto);
    Task<bool> CleanupExpiredHomeworksAsync();

}