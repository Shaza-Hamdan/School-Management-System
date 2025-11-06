using Trial.DTO;
using TRIAL.Persistence.entity;

namespace TRIAL.Services
{
    public interface ISubjectsService
    {
        Task<IEnumerable<SubjectRetrieving>> GetSubjectsAsync();
        Task<SubjectDetails> GetSubjectDetailAsync(int subjectId);
        Task<bool> DeleteSubjectAsync(int subjectId);
        Task<string> AddNewSubject(AddNewSubject subject);
        Task<bool> UpdateSubject(UpdateSubject UpSub);
    }
}