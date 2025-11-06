using System.Linq;
using Microsoft.EntityFrameworkCore;
using Trial.DTO;
using TRIAL.Persistence.entity;
using TRIAL.Persistence.Repository;


namespace TRIAL.Services.Implementations
{
    public class MarksService : IMarksService
    {
        private readonly AppDBContext appdbContext;
        public MarksService(AppDBContext appDbContext)
        {
            appdbContext = appDbContext;
        }

        public async Task<string> AddMarks(AddMark dto)
        {
            var student = await appdbContext.registrations.FindAsync(dto.RegistrationId);
            if (student == null)
                return "Student not found.";

            var subject = await appdbContext.subjectNa.FindAsync(dto.subjectsId);
            if (subject == null)
                return "Subject not found.";

            var mark = new Marks
            {
                RegistrationId = dto.RegistrationId,
                subjectsId = dto.subjectsId,
                Oral = dto.oral,
                Written = dto.written
            };

            appdbContext.marks.Add(mark);
            await appdbContext.SaveChangesAsync();

            return "Marks added successfully.";
        }

        public async Task<object?> GetTheMark(GetMark dto)
        {
            var student = await appdbContext.registrations.FindAsync(dto.RegistrationId);
            if (student == null)
                return "Student not found.";

            var subject = await appdbContext.subjectNa.FindAsync(dto.subjectsId);
            if (subject == null)
                return "Subject not found.";

            var mark = await appdbContext.marks.FirstOrDefaultAsync(h => h.RegistrationId == dto.RegistrationId && h.subjectsId == dto.subjectsId);
            if (mark == null) return "mark not found";
            return new MarksResults(mark.RegistrationId, mark.subjectsId, mark.Written, mark.Oral);
        }
    }
}