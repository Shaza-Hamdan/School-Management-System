using Trial.DTO;
using TRIAL.Persistence.entity;
using TRIAL.Persistence.Repository;
using System.Data;
using Microsoft.EntityFrameworkCore;


namespace TRIAL.Services.Implementations
{
    public class SubjectsService : ISubjectsService
    {
        private readonly AppDBContext appdbContext;
        public SubjectsService(AppDBContext appDbContext)
        {
            appdbContext = appDbContext;
        }

        public async Task<IEnumerable<SubjectRetrieving>> GetSubjectsAsync() // returns a collection  of lists 
        {
            var sub = await appdbContext.subjectNa.ToListAsync();
            return sub.Select(subs => new SubjectRetrieving(
                        subs.SubName,
                        subs.Discription,
                        subs.RegistrationId
                    ));
        }

        public async Task<SubjectDetails> GetSubjectDetailAsync(int subjectId)
        {
            var subject = await appdbContext.subjectNa
                .Include(s => s.marks)
                .Include(s => s.homeworkTs)
                .FirstOrDefaultAsync(s => s.Id == subjectId);

            if (subject == null)
            {
                return null;
            }

            return new SubjectDetails(subject.Id, subject.SubName, subject.Discription, subject.RegistrationId);
        }

        public async Task<bool> DeleteSubjectAsync(int subjectId)
        {
            var subject = await appdbContext.subjectNa
           .FirstOrDefaultAsync(s => s.Id == subjectId);

            if (subject == null)
            {
                return false;
            }

            appdbContext.subjectNa.Remove(subject);
            await appdbContext.SaveChangesAsync();
            return true;
        }

        public async Task<string> AddNewSubject(AddNewSubject subject)
        {
            try
            {
                var newSubject = new Subjects
                {
                    SubName = subject.SubName,
                    Discription = subject.Discription,
                    RegistrationId = subject.RegistrationId
                };
                appdbContext.subjectNa.Add(newSubject);
                await appdbContext.SaveChangesAsync();
                return "Successfully added the new subject.";
            }
            catch (Exception)
            {
                return "Couldn't add new subject";
            }
        }

        public async Task<bool> UpdateSubject(UpdateSubject UpSub)
        {
            var subject = await appdbContext.subjectNa.FindAsync(UpSub.Id);
            if (subject == null)
            {
                return false;
            }

            subject.SubName = UpSub.SubName;
            subject.Discription = UpSub.Discription;
            subject.RegistrationId = UpSub.RegistrationId;
            appdbContext.subjectNa.Update(subject);
            await appdbContext.SaveChangesAsync();

            return true;
        }

    }

}





